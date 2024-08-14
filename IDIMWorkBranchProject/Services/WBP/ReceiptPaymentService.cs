using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ReceiptPaymentService : IReceiptPaymentService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ReceiptPaymentService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ReceiptPayment> GetAll()
        {
            return Context.ReceiptPayments.OrderByDescending(e => e.ReceiptPaymentId).AsQueryable();
        }

        public async Task<List<ReceiptPaymentVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ReceiptPaymentVm>>(list);
        }

        public async Task<ReceiptPaymentVm> GetByIdAsync(int id)
        {
            var entity = await Context.ReceiptPayments.FindAsync(id);

            return Mapper.Map<ReceiptPaymentVm>(entity);
        }


        public async Task<ReceiptPaymentVm> InsertAsync(ReceiptPaymentVm model)
        {
            var entity = Mapper.Map<ReceiptPayment>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ReceiptPayments.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ReceiptPaymentVm>(added);
        }

        public async Task<ReceiptPaymentVm> UpdateAsync(ReceiptPaymentVm model)
        {
            var existing = await Context.ReceiptPayments.FirstOrDefaultAsync(e => e.ProjectId == model.ProjectId);
            if (existing == null)
                throw new ArgumentException($"Receipt Payment does not exists.");

            existing.FiscalYearId = model.FiscalYearId;
            existing.QuarterId = model.QuarterId;
            existing.DocumentType = (int)model.DocumentType;
            existing.SourceType = (int)model.SourceType;
            existing.ReceiptAmount = model.ReceiptAmount;
            existing.ReceiptDate = model.ReceiptDate;
            existing.Remark = model.Remark;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ReceiptPaymentVm>(existing);
        }


        public async Task<ReceiptPaymentVm> DeleteAsync(int id)
        {
            var existing = await Context.ReceiptPayments.FirstOrDefaultAsync(e => e.ReceiptPaymentId == id);
            if (existing == null)
                throw new ArgumentException($"Receipt Payment does not exists.");

            Context.ReceiptPayments.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ReceiptPaymentVm>(existing);
        }

        public async Task<List<ReceiptPaymentVm>> GetByAsync(ReceiptPaymentSearchVm filter = null)
        {
            if (filter == null)
                filter = new ReceiptPaymentSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ReceiptPaymentVm>>(list);
        }
    }
}