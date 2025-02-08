using AutoMapper;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using IDIMWorkBranchProject.Data.Database;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class VatTaxService : IVatTaxService
    {
        protected IDIMDBEntities _context;
        protected IMapper _mapper;

        public VatTaxService(IMapper mapper)
        {
            _context = new IDIMDBEntities();
            _mapper = mapper;
        }

        private IQueryable<VatTax> GetAll()
        {
            return _context.VatTaxes.OrderByDescending(e => e.VatTaxId).AsQueryable();
        }

        public async Task<List<VatTaxVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return _mapper.Map<List<VatTaxVm>>(list);
        }

        public async Task<VatTaxVm> GetByIdAsync(int id)
        {
            var entity = await _context.VatTaxes.FindAsync(id);

            return _mapper.Map<VatTaxVm>(entity);
        }


        public async Task<VatTaxVm> InsertAsync(VatTaxVm model)
        {
            var entity = _mapper.Map<VatTax>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = _context.VatTaxes.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<VatTaxVm>(added);
        }

        public async Task<VatTaxVm> UpdateAsync(VatTaxVm model)
        {
            var existing = await _context.VatTaxes
                .Where(e => e.VatTaxId == model.VatTaxId)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"VatTax does not exists.");

            existing.ProjectId = model.ProjectId;
            existing.ReceivePaymentId = model.ReceivePaymentId;
            existing.TaxPer = model.TaxPer;
            existing.TaxAmount = model.TaxAmount;
            existing.VatPer = model.VatPer;
            existing.VatAmount = model.VatAmount;
            existing.CollateralPer = model.CollateralPer;
            existing.CollateralAmount = model.CollateralAmount;
            existing.Remarks = model.Remarks;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await _context.SaveChangesAsync();

            return _mapper.Map<VatTaxVm>(existing);
        }

        public async Task<VatTaxVm> DeleteAsync(int id)
        {
            var existing = await _context.VatTaxes
                .Where(e => e.VatTaxId == id)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"VatTax does not exists.");

            _context.VatTaxes.Remove(existing);
            await _context.SaveChangesAsync();

            return _mapper.Map<VatTaxVm>(existing);
        }

        public async Task<object> GetByAsync(VatTaxSearchVm filter = null)
        {
            if (filter == null)
                filter = new VatTaxSearchVm();

            // Querying SubProjects and including related Project
            var data = _context.VatTaxes.Include(x => x.Project).Include(x => x.ReceivePayment).AsQueryable();

            var query = data.Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                    (string.IsNullOrEmpty(filter.LetterNumber) || x.ReceivePayment.LetterNo.Contains(filter.LetterNumber)));

            // Sorting: Dynamically apply sorting based on DataTable's request
            if (!string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection))
            {
                // Use dynamic OrderBy from System.Linq.Dynamic
                query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
            }
            else
            {
                // If no sort is specified, apply a default order (e.g., by ID)
                query = query.OrderBy(x => x.VatTaxId);  // Default ordering by SignatoryAuthorityId
            }

            // Pagination: Apply pagination based on DataTable's filter
            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.Skip(filter.PageIndex * filter.PageSize)
                                              .Take(filter.PageSize)
                                              .ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = filter.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords.Count(),
                data = filteredRecords.Select(x => new VatTaxVm
                {
                    VatTaxId = x.VatTaxId,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.ProjectName,
                    ReceivePaymentId = x.ReceivePaymentId,
                    LetterNumber = x.ReceivePayment.LetterNo,
                    TaxPer = x.TaxPer,
                    TaxAmount = x.TaxAmount,
                    VatPer = x.VatPer,
                    VatAmount = x.VatAmount,
                    CollateralPer = x.CollateralPer,
                    CollateralAmount = x.CollateralAmount,
                    Remarks = x.Remarks,
                })
            };

            return result;
        }

        public async Task<int> GetByProjectAndPaymentAsync(int projectId, int receivePaymentId)
        {
            return await _context.VatTaxes
                .Where(x => x.ProjectId == projectId && x.ReceivePaymentId == receivePaymentId)
                .Select(x => x.VatTaxId)
                .FirstOrDefaultAsync();
        }
    }
}