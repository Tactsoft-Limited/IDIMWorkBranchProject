using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class BillPaymentService : IBillPaymentService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public BillPaymentService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<BillPayment> GetAll()
        {
            return Context.BillPayments.OrderByDescending(e => e.BillPaymentId).AsQueryable();
        }

        public async Task<List<BillPaymentVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<BillPaymentVm>>(list);
        }

        public async Task<BillPaymentVm> GetByIdAsync(int id)
        {
            var entity = await Context.BillPayments.FindAsync(id);

            return Mapper.Map<BillPaymentVm>(entity);
        }


        public async Task<BillPaymentVm> InsertAsync(BillPaymentVm model)
        {
            var entity = Mapper.Map<BillPayment>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.BillPayments.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<BillPaymentVm>(added);
        }

        public async Task<BillPaymentVm> UpdateAsync(BillPaymentVm model)
        {
            var existing = await Context.BillPayments
                .Where(e => e.BillPaymentId == model.BillPaymentId)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Bill Payment does not exists.");

            existing.FiscalYearId = model.FiscalYearId;
            existing.BillTypeId = model.BillTypeId;
            existing.SourceType = (int)model.SourceType;
            existing.PaymentType = (int)model.PaymentType;
            existing.PaymentAmount = model.PaymentAmount;
            existing.PaymentDate = model.PaymentDate;
            existing.BankName = model.BankName;
            existing.BranchName = model.BranchName;
            existing.AccountName = model.AccountName;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<BillPaymentVm>(existing);
        }

        public async Task<BillPaymentVm> DeleteAsync(int id)
        {
            var existing = await Context.BillPayments
                .Where(e => e.BillPaymentId == id)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Bill Payment does not exists.");

            Context.BillPayments.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<BillPaymentVm>(existing);
        }

        public async Task<List<BillPaymentVm>> GetByAsync(BillPaymentSearchVm filter = null)
        {
            if (filter == null)
                filter = new BillPaymentSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProject.SubProjectTitle.Contains(filter.SubProjectTitle)) &&
                    (!filter.FiscalYearId.HasValue || x.FiscalYearId == filter.FiscalYearId) &&
                    (!filter.BillTypeId.HasValue || x.BillTypeId == filter.BillTypeId) &&
                    (!filter.SourceType.HasValue || x.SourceType == (int)filter.SourceType.Value))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<BillPaymentVm>>(list);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var billPayments = await Context.BillPayments.ToListAsync();

            return billPayments.Select(e => new SelectListItem
            {
                Text = e.BranchName,
                Value = e.BillPaymentId.ToString(),
                Selected = e.BillPaymentId == selected
            });
        }
    }
}