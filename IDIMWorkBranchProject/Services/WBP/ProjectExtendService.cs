using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IDIMWorkBranchProject.Entity;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ProjectExtendService : IProjectExtendService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ProjectExtendService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ProjectExtend> GetAll()
        {
            return Context.ProjectExtends.OrderByDescending(e => e.ProjectExtendId).AsQueryable();
        }

        public async Task<List<ProjectExtendVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ProjectExtendVm>>(list);
        }

        public async Task<ProjectExtendVm> GetByIdAsync(int id)
        {
            var entity = await Context.ProjectExtends.FindAsync(id);

            return Mapper.Map<ProjectExtendVm>(entity);
        }


        public async Task<ProjectExtendVm> InsertAsync(ProjectExtendVm model)
        {

            var entity = Mapper.Map<ProjectExtend>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ProjectExtends.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectExtendVm>(added);
        }

        public async Task<ProjectExtendVm> UpdateAsync(ProjectExtendVm model)
        {
            var existing = await Context.ProjectExtends.FirstOrDefaultAsync(e => e.SubProjectId == model.SubProjectId);
            if (existing == null)
                throw new ArgumentException($"Project Extend  does not exists.");

            existing.NoOfDays = model.NoOfDays;
            existing.ExtendDate = model.ExtendDate;
            existing.NewHandOverDate = model.NewHandOverDate;
            existing.AttachmentId = model.AttachmentId;
            existing.Reason = model.Reason;
            existing.Remark = model.Remark;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectExtendVm>(existing);
        }

        public async Task<List<ProjectExtendVm>> GetByAsync(ProjectExtendSearchVm filter = null)
        {
            if (filter == null)
                filter = new ProjectExtendSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProject.SubProjectTitle.Contains(filter.SubProjectTitle)))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ProjectExtendVm>>(list);
        }

        public async Task<ProjectExtendVm> DeleteAsync(int id)
        {
            var existing = await Context.ProjectExtends.FirstOrDefaultAsync(e => e.SubProjectId == id);
            if (existing == null)
                throw new ArgumentException($"Project Extend does not exists.");

            Context.ProjectExtends.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectExtendVm>(existing);
        }
    }
}