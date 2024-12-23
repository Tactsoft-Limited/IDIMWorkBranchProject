using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ContractorPaymentService : IContractorPaymentService
    {
        protected readonly IDIMDBEntities _context;
        protected readonly IMapper _mapper;

        public ContractorPaymentService(IDIMDBEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContractorPaymentVm> DeleteAsync(int id)
        {
            var existing = await _context.ContractorPayments.FirstOrDefaultAsync(x => x.ContractorPaymentId == id);

            if (existing == null)
                throw new ArgumentException($"Contractor Payment does not exists.");

            _context.ContractorPayments.Remove(existing);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractorPaymentVm>(existing);
        }

        public async Task<List<ContractorPaymentVm>> GetAllAsync()
        {
            var list = await _context.ContractorPayments.Take(DefaultData.Take).ToListAsync();
            return _mapper.Map<List<ContractorPaymentVm>>(list);
        }

        public async Task<object> GetByAsync(ContractorPaymentSearchVm filter = null)
        {
            if (filter == null)
                filter = new ContractorPaymentSearchVm();

            // Querying SubProjects and including related Project
            var data = _context.ContractorPayments.Include(x => x.SubProject).Include(x => x.ConstructionFirm).AsQueryable();

            var query = data.Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProject.SubProjectTitle.Contains(filter.SubProjectTitle)) &&
                    (string.IsNullOrEmpty(filter.ConstructionFirmName) || x.ConstructionFirm.ConstructionFirmName.Contains(filter.ConstructionFirmName)));

            // Sorting: Dynamically apply sorting based on DataTable's request
            if (!string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection))
            {
                // Use dynamic OrderBy from System.Linq.Dynamic
                query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
            }
            else
            {
                // If no sort is specified, apply a default order (e.g., by ID)
                query = query.OrderBy(x => x.ContractorPaymentId);  // Default ordering by SignatoryAuthorityId
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
                data = filteredRecords.Select(x => new ContractorPaymentVm
                {
                    ContractorPaymentId = x.ContractorPaymentId,
                    SubProjectId = x.SubProjectId,
                    SubProjectTitle = x.SubProject.SubProjectTitle,
                    ConstructionFirmId = x.ConstructionFirmId,
                    ConstructionFirmName = x.ConstructionFirm.ConstructionFirmName,
                    ApprovalAmount = x.ApprovalAmount,
                    VatTaxSecurityAmount = x.VatTaxSecurityAmount,
                    NetAmount = x.NetAmount,
                    ProgressPer = x.ProgressPer,
                    ProgressAmount = x.ProgressAmount,
                    DeductionAmount = x.DeductionAmount,
                    NetDeductionAmount = x.NetDeductionAmount,
                    PerformanceSecurityPer = x.PerformanceSecurityPer,
                    PerformanceSecurityAmount = x.PerformanceSecurityAmount,
                    ContactorProgressPer = x.ContactorProgressPer,
                    ContactorProgressAmount = x.ContactorProgressAmount,
                    BillPaymentNumber = x.BillPaymentNumber,
                    RunningBillPayment = x.RunningBillPayment,
                    ContactorDepositAmount = x.ContactorDepositAmount,
                    ContactorFinalPaymentAmount = x.ContactorFinalPaymentAmount,
                    BGBFundDepositAmount = x.BGBFundDepositAmount,
                    Remarks = x.Remarks,
                })
            };

            return result;
        }

        public async Task<ContractorPaymentVm> GetByIdAsync(int id)
        {
            var entity = await _context.ContractorPayments.FindAsync(id);
            return _mapper.Map<ContractorPaymentVm>(entity);
        }

        public Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ContractorPaymentVm> InsertAsync(ContractorPaymentVm model)
        {
            var entity = _mapper.Map<ContractorPayment>(model);
            entity.CreatedUser = UserExtention.GetUserId();
            entity.CreatedDateTime = DateTime.Now;

            var added = _context.ContractorPayments.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractorPaymentVm>(added);
        }

        public async Task<ContractorPaymentVm> UpdateAsync(ContractorPaymentVm model)
        {
            // Find the existing ContractorPayment in the database
            var existing = await _context.ContractorPayments
                                                              .FirstOrDefaultAsync(c => c.ContractorPaymentId == model.ContractorPaymentId);
            if (existing == null)
            {
                throw new KeyNotFoundException("ContractorPayment not found.");
            }

            // Update the entity's properties
            existing.SubProjectId = model.SubProjectId;
            existing.ConstructionFirmId = model.ConstructionFirmId;
            existing.ApprovalAmount = model.ApprovalAmount;
            existing.VatPer = model.VatPer;
            existing.TaxPer = model.TaxPer;
            existing.CollateralPer = model.CollateralPer;
            existing.VatTaxSecurityAmount = model.VatTaxSecurityAmount;
            existing.NetAmount = model.NetAmount;
            existing.ProgressPer = model.ProgressPer;
            existing.ProgressAmount = model.ProgressAmount;
            existing.ProgressTaxPer = model.ProgressTaxPer;
            existing.ProgressVatPer = model.ProgressVatPer;
            existing.ProgressCollateralPer = model.ProgressCollateralPer;
            existing.DeductionAmount = model.DeductionAmount;
            existing.NetDeductionAmount = model.NetDeductionAmount;
            existing.PerformanceSecurityPer = model.PerformanceSecurityPer;
            existing.PerformanceSecurityAmount = model.PerformanceSecurityAmount;
            existing.ContactorProgressPer = model.ContactorProgressPer;
            existing.ContactorProgressAmount = model.ContactorProgressAmount;
            existing.BillPaymentNumber = model.BillPaymentNumber;
            existing.RunningBillPayment = model.RunningBillPayment;
            existing.ContactorDepositAmount = model.ContactorDepositAmount;
            existing.ContactorFinalPaymentAmount = model.ContactorFinalPaymentAmount;
            existing.BGBFundDepositAmount = model.BGBFundDepositAmount;
            existing.Remarks = model.Remarks;

            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdatedDateTime = DateTime.UtcNow;
            existing.UpdateNo += 1;

            // Save the updated entity to the database
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractorPaymentVm>(existing);
        }
    }
}