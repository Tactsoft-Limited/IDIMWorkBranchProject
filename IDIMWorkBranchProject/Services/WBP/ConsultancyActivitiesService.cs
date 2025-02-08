using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;
namespace IDIMWorkBranchProject.Services.WBP
{
    public class ConsultancyActivitiesService : IConsultancyActivitiesService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ConsultancyActivitiesService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ConsultancyActivity> GetAll()
        {
            return Context.ConsultancyActivities.OrderByDescending(e => e.CAId).AsQueryable();
        }

        public async Task<List<ConsultancyActivitiesVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ConsultancyActivitiesVm>>(list);
        }

        public async Task<ConsultancyActivitiesVm> GetByIdAsync(int id)
        {
            var entity = await Context.ConsultancyActivities.FindAsync(id);

            return Mapper.Map<ConsultancyActivitiesVm>(entity);
        }


        public async Task<ConsultancyActivitiesVm> InsertAsync(ConsultancyActivitiesVm model)
        {

            var entity = Mapper.Map<ConsultancyActivity>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ConsultancyActivities.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyActivitiesVm>(added);
        }

        public async Task<ConsultancyActivitiesVm> UpdateAsync(ConsultancyActivitiesVm model)
        {
            var existing = await Context.ConsultancyActivities.FirstOrDefaultAsync(e => e.CAId == model.CAId);
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.SubProjectId = model.SubProjectId;
            existing.ConsultantId = model.ConsultantId;
            existing.Progress = model.Progress;
            existing.ConsultantProgressForSupervission = model.ConsultantProgressForSupervission;
            existing.ProgressAccordingToContract = model.ProgressAccordingToContract;
            existing.AgreedMoney = model.AgreedMoney;
            existing.PaidMoney = model.PaidMoney;
            existing.PayableForProgress = model.PayableForProgress;
            existing.RestOfPayableForProgress = model.RestOfPayableForProgress;
            existing.Remark = model.Remark;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyActivitiesVm>(existing);
        }

        public async Task<List<ConsultancyActivitiesVm>> GetByAsync(ConsultancyActivitiesSearchVm filter = null)
        {
            if (filter == null)
                filter = new ConsultancyActivitiesSearchVm();

            var query = GetAll().Where(x =>
                    (!filter.SubProjectId.HasValue || x.SubProjectId == filter.SubProjectId) &&
                    (!filter.ConsultantId.HasValue || x.ConsultantId == filter.ConsultantId))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ConsultancyActivitiesVm>>(list);
        }

        public async Task<ConsultancyActivitiesVm> DeleteAsync(int id)
        {
            var existing = await Context.ConsultancyActivities.FirstOrDefaultAsync(e => e.CAId == id);
            if (existing == null)
                throw new ArgumentException($"Project Status does not exists.");

            Context.ConsultancyActivities.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultancyActivitiesVm>(existing);
        }
    }
}