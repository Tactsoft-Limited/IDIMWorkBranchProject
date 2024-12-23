using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class SecurityDepositService : ISecurityDepositService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public SecurityDepositService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<SecurityDeposit> GetAll()
        {
            return Context.SecurityDeposits.OrderByDescending(e => e.SecurityDepositId).AsQueryable();
        }

        public async Task<List<SecurityDepositVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<SecurityDepositVm>>(list);
        }

        public async Task<SecurityDepositVm> GetByIdAsync(int id)
        {
            var entity = await Context.SecurityDeposits.FindAsync(id);

            return Mapper.Map<SecurityDepositVm>(entity);
        }


        public async Task<SecurityDepositVm> InsertAsync(SecurityDepositVm model)
        {

            var entity = Mapper.Map<SecurityDeposit>(model);
            //entity.CreatedDateTime = DateTime.Now;
            //entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.SecurityDeposits.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<SecurityDepositVm>(added);
        }

        public async Task<SecurityDepositVm> UpdateAsync(SecurityDepositVm model)
        {
            var existing = await Context.SecurityDeposits.FirstOrDefaultAsync(e => e.SecurityDepositId == model.SecurityDepositId);
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.SubProjectId = model.SubProjectId;
            existing.SecurityAmount = model.SecurityAmount;
            existing.LetterDate = model.LetterDate;
            existing.LetterNumber = model.LetterNumber;
            existing.Remark = model.Remark;

            //existing.UpdatedDateTime = DateTime.Now;
            //existing.UpdatedUser = UserExtention.GetUserId();
            await Context.SaveChangesAsync();

            return Mapper.Map<SecurityDepositVm>(existing);
        }

        public async Task<List<SecurityDepositVm>> GetByAsync(SecurityDepositSearchVm filter = null)
        {
            if (filter == null)
                filter = new SecurityDepositSearchVm();

            var query = GetAll().Where(x =>
                    (!filter.SubProjectId.HasValue || x.SubProjectId == filter.SubProjectId) &&
                    (filter.LetterNumber == null || x.LetterNumber == filter.LetterNumber))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<SecurityDepositVm>>(list);
        }

        public async Task<SecurityDepositVm> DeleteAsync(int id)
        {
            var existing = await Context.SecurityDeposits.FirstOrDefaultAsync(e => e.SecurityDepositId == id);
            if (existing == null)
                throw new ArgumentException($"Security deposit does not exists.");

            Context.SecurityDeposits.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<SecurityDepositVm>(existing);
        }
    }
}