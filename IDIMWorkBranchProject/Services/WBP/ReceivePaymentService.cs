using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ReceivePaymentService : IReceivePaymentService
    {
        protected IDIMDBEntities _context;
        protected IMapper _mapper;

        public ReceivePaymentService(IMapper mapper)
        {
            _context = new IDIMDBEntities();
            _mapper = mapper;
        }

        private IQueryable<ReceivePayment> GetAll()
        {
            return _context.ReceivePayments.OrderByDescending(e => e.ReceivePaymentId).AsQueryable();
        }

        public async Task<List<ReceivePaymentVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return _mapper.Map<List<ReceivePaymentVm>>(list);
        }
        public async Task<object> GetAllAsync(ReceivePaymentSearchVm filter)
        {
            try
            {
                if (filter == null)
                    filter = new ReceivePaymentSearchVm();

                // Apply filters to the query
                var query = _context.ReceivePayments.Include(x => x.Project).Include(x => x.ConstructionFirm).Where(x =>
                        (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                        (string.IsNullOrEmpty(filter.ConstructionFirmName) || x.ConstructionFirm.ConstructionFirmName.Contains(filter.ConstructionFirmName)) &&
                        (!filter.ProjectId.HasValue || x.ProjectId == filter.ProjectId) &&
                        (!filter.ConstructionFirmId.HasValue || x.ConstructionFirmId == filter.ConstructionFirmId) &&
                        (!filter.BillDate.HasValue || DbFunctions.TruncateTime(x.BillDate) == DbFunctions.TruncateTime(filter.BillDate)));

                // Sorting: Dynamically apply sorting based on DataTable's request
                query = !string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection)
                    ? query.OrderBy($"{filter.SortColumn} {filter.SortDirection}") : query.OrderBy(x => x.ReceivePaymentId);


                // Pagination: Apply pagination based on DataTable's filter
                var totalRecords = await query.CountAsync();
                var filteredRecords = await query.CountAsync();

                //var pagedData = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

                // Return the response in DataTables format
                return new
                {
                    draw = filter.Draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = filteredRecords,
                    data = query.Select(x => new ReceivePaymentVm
                    {
                        ReceivePaymentId = x.ReceivePaymentId,
                        ProjectId = x.ProjectId,
                        ProjectName = x.Project.ProjectName,
                        ConstructionFirmId = x.ConstructionFirmId,
                        ConstructionFirmName = x.ConstructionFirm.ConstructionFirmName,
                        LetterNo = x.LetterNo,
                        BillDate = x.BillDate,
                        ExtraTime = x.ExtraTime,
                        BillPaymentSector = x.BillPaymentSector,
                        Budget = x.Budget,
                        WorkProgress = x.WorkProgress,
                        FinancialProgress = x.FinancialProgress,
                        BillNumber = x.BillNumber,
                        BillAmount = x.BillAmount,
                        BillPercentage = x.BillPercentage,
                    })
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle accordingly
                Console.WriteLine("Error: " + ex.Message);
                return new { error = "An error occurred while fetching data." };
            }

        }


        public async Task<ReceivePaymentVm> GetByIdAsync(int id)
        {
            var entity = await _context.ReceivePayments.FindAsync(id);

            return _mapper.Map<ReceivePaymentVm>(entity);
        }


        public async Task<ReceivePaymentVm> InsertAsync(ReceivePaymentVm model)
        {
            var entity = _mapper.Map<ReceivePayment>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = _context.ReceivePayments.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<ReceivePaymentVm>(added);
        }

        public async Task<ReceivePaymentVm> UpdateAsync(ReceivePaymentVm model)
        {
            var existing = await _context.ReceivePayments.FirstOrDefaultAsync(e => e.ProjectId == model.ProjectId);

            if (existing == null)
                throw new ArgumentException($"Receipt Payment does not exists.");

            existing.ProjectId = model.ProjectId;
            existing.LetterNo = model.LetterNo;
            existing.BillDate = model.BillDate;
            existing.ExtraTime = model.ExtraTime;
            existing.BillPaymentSector = model.BillPaymentSector;
            existing.Budget = model.Budget;
            existing.WorkProgress = model.WorkProgress;
            existing.FinancialProgress = model.FinancialProgress;
            existing.BillNumber = model.BillNumber;
            existing.BillPercentage = model.BillPercentage;
            existing.BillAmount = model.BillAmount;
            existing.Remarks = model.Remarks;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await _context.SaveChangesAsync();

            return _mapper.Map<ReceivePaymentVm>(existing);
        }


        public async Task<ReceivePaymentVm> DeleteAsync(int id)
        {
            var existing = await _context.ReceivePayments.FirstOrDefaultAsync(e => e.ReceivePaymentId == id);
            if (existing == null)
                throw new ArgumentException($"Receipt Payment does not exists.");

            _context.ReceivePayments.Remove(existing);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReceivePaymentVm>(existing);
        }

        public async Task<List<ReceivePaymentVm>> GetByAsync(ReceivePaymentSearchVm filter = null)
        {
            if (filter == null)
                filter = new ReceivePaymentSearchVm();

            var result = await _context.ReceivePayments.Include(x => x.Project).Include(x => x.ConstructionFirm).ToListAsync();

            var query = _context.ReceivePayments.Include(x => x.Project).Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)));

            var list = await query.ToListAsync();

            return _mapper.Map<List<ReceivePaymentVm>>(list);
        }

        public async Task<int> GetTotalPaymentReceiveAsync(int id)
        {
            return await _context.ReceivePayments.Where(x => x.ProjectId == id).CountAsync() + 1;
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            return await _context.ReceivePayments.Select(x => new SelectListItem
            {
                Text = x.LetterNo,
                Value = x.ReceivePaymentId.ToString(),
                Selected = x.ReceivePaymentId == selected
            }).ToListAsync();
        }
    }
}