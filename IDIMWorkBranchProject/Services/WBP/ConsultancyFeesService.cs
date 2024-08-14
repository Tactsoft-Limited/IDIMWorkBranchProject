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
    public class ConsultancyFeesService : IConsultancyFeesService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ConsultancyFeesService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ConsultancyFee> GetAll()
        {
            return Context.ConsultancyFees.OrderByDescending(e => e.CFId).AsQueryable();
        }

        public async Task<List<ConsultancyFeesVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ConsultancyFeesVm>>(list);
        }

        public async Task<ConsultancyFeesVm> GetByIdAsync(int id)
        {
            var entity = await Context.ConsultancyFees.FindAsync(id);

            return Mapper.Map<ConsultancyFeesVm>(entity);
        }


        public async Task<ConsultancyFeesVm> InsertAsync(ConsultancyFeesVm model)
        {

            var entity = Mapper.Map<ConsultancyFee>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ConsultancyFees.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyFeesVm>(added);
        }

        public async Task<ConsultancyFeesVm> UpdateAsync(ConsultancyFeesVm model)
        {
            var existing = await Context.ConsultancyFees.FirstOrDefaultAsync(e => e.CFId == model.CFId);
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.SubProjectId = model.SubProjectId;
            existing.ConsultantId = model.ConsultantId;
            existing.ConsultancyFee1 = model.ConsultancyFee;
            existing.Vat_Tax = model.Vat_Tax;
            existing.NitAmmountConsultancyFee = model.NitAmmountConsultancyFee;
            existing.OtherConsultancyFee = model.OtherConsultancyFee;
            existing.RestAmount = model.RestAmount;
            existing.Remark = model.Remark;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyFeesVm>(existing);
        }

        public async Task<List<ConsultancyFeesVm>> GetByAsync(ConsultancyFeesSearchVm filter = null)
        {
            if (filter == null)
                filter = new ConsultancyFeesSearchVm();

            var query = GetAll().Where(x =>
                    (!filter.SubProjectId.HasValue || x.SubProjectId == filter.SubProjectId) &&
                    (!filter.ConsultantId.HasValue || x.ConsultantId == filter.ConsultantId))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ConsultancyFeesVm>>(list);
        }

        public async Task<ConsultancyFeesVm> DeleteAsync(int id)
        {
            var existing = await Context.ConsultancyFees.FirstOrDefaultAsync(e => e.CFId == id);
            if (existing == null)
                throw new ArgumentException($"Project Status does not exists.");

            Context.ConsultancyFees.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyFeesVm>(existing);
        }
    }
}