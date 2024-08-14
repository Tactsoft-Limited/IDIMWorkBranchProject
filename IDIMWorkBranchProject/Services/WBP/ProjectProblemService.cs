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
    public class ProjectProblemService : IProjectProblemService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ProjectProblemService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ProjectProblem> GetAll()
        {
            return Context.ProjectProblems.OrderByDescending(e => e.ProjectProblemId).AsQueryable();
        }

        public async Task<List<ProjectProblemVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ProjectProblemVm>>(list);
        }

        public async Task<ProjectProblemVm> GetByIdAsync(int id)
        {
            var entity = await Context.ProjectProblems.FindAsync(id);

            return Mapper.Map<ProjectProblemVm>(entity);
        }


        public async Task<ProjectProblemVm> InsertAsync(ProjectProblemVm model)
        {
            var entity = Mapper.Map<ProjectProblem>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ProjectProblems.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectProblemVm>(added);
        }

        public async Task<ProjectProblemVm> UpdateAsync(ProjectProblemVm model)
        {
            var existing = await Context.ProjectProblems.FirstOrDefaultAsync(e => e.SubProjectId == model.SubProjectId);
            if (existing == null)
                throw new ArgumentException($"Project Problem does not exists.");

            existing.Description = model.Description;
            existing.IssueDate = model.IssueDate;
            existing.Status = model.Status;
            existing.SolveDate = model.SolveDate;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectProblemVm>(existing);
        }

        public async Task<List<ProjectProblemVm>> GetByAsync(ProjectProblemSearchVm filter = null)
        {
            if (filter == null)
                filter = new ProjectProblemSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProject.SubProjectTitle.Contains(filter.SubProjectTitle)))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ProjectProblemVm>>(list);
        }

        public async Task<ProjectProblemVm> DeleteAsync(int id)
        {
            var existing = await Context.ProjectProblems.FirstOrDefaultAsync(e => e.SubProjectId == id);
            if (existing == null)
                throw new ArgumentException($"Project Problem does not exists.");

            Context.ProjectProblems.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectProblemVm>(existing);
        }
    }
}