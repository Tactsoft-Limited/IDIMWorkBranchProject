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
    public class SubProjectDetailsService : ISubProjectDetailsService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public SubProjectDetailsService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<SubProjectDetail> GetAll()
        {
            return Context.SubProjectDetails.OrderByDescending(e => e.SubProjectDetailsId).AsQueryable();
        }

        public async Task<List<SubProjectDetailsVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<SubProjectDetailsVm>>(list);
        }

        public async Task<SubProjectDetailsVm> GetByIdAsync(int id)
        {
            var entity = await Context.SubProjectDetails.FindAsync(id);

            return Mapper.Map<SubProjectDetailsVm>(entity);
        }


        public async Task<SubProjectDetailsVm> InsertAsync(SubProjectDetailsVm model)
        {

            var entity = Mapper.Map<SubProjectDetail>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.SubProjectDetails.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectDetailsVm>(added);
        }

        public async Task<SubProjectDetailsVm> UpdateAsync(SubProjectDetailsVm model)
        {
            var existing = await Context.SubProjectDetails.FirstOrDefaultAsync(e => e.SubProjectDetailsId == model.SubProjectDetailsId);
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.SubProjectId = model.SubProjectId;
            existing.hasCompleted = model.hasCompleted;
            existing.BGOrPayOrder = model.BGOrPayOrder;
            existing.BGConfirmed = model.BGConfirmed;
            existing.ContractComplete = model.ContractComplete;
            existing.WorkOrderDate = model.WorkOrderDate;
            existing.DesignSent = model.DesignSent;
            existing.Installment1 = model.Installment1;
            existing.Installment2 = model.Installment2;
            existing.Installment4 = model.Installment4;
            existing.Installment5 = model.Installment5;
            existing.TotalAmmount = model.TotalAmmount;
            existing.BGBMiscellaneousDeposit = model.BGBMiscellaneousDeposit;
            existing.Progress = model.Progress;
            existing.IsDelivered = model.IsDelivered;
            existing.IsFinalBillPaid = model.IsFinalBillPaid;
            existing.TenPercentGuarantee = model.TenPercentGuarantee;
            existing.FivePercentSecurityMoney = model.FivePercentSecurityMoney;
            existing.TenPercentPaidFarm = model.TenPercentPaidFarm;
            existing.TenPercentPaidSimantoFund = model.TenPercentPaidSimantoFund;
            existing.ContractorPayable = model.ContractorPayable;
            existing.Remark = model.Remark;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectDetailsVm>(existing);
        }

        public async Task<List<SubProjectDetailsVm>> GetByAsync(SubProjectDetailsSearchVm filter = null)
        {
            if (filter == null)
                filter = new SubProjectDetailsSearchVm();

            var query = GetAll().Where(x =>
                    (!filter.SubProjectId.HasValue || x.SubProjectId == filter.SubProjectId))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<SubProjectDetailsVm>>(list);
        }

        public async Task<SubProjectDetailsVm> DeleteAsync(int id)
        {
            var existing = await Context.SubProjectDetails.FirstOrDefaultAsync(e => e.SubProjectDetailsId == id);
            if (existing == null)
                throw new ArgumentException($"Project Status does not exists.");

            Context.SubProjectDetails.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectDetailsVm>(existing);
        }
        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var subProjects = await Context.SubProjects.ToListAsync();

            return subProjects.Select(e => new SelectListItem
            {
                Text = $"{e.SubProjectId} - {e.SubProjectTitle}",
                Value = e.SubProjectId.ToString(),
                Selected = e.SubProjectId == selected
            });
        }
    }
}