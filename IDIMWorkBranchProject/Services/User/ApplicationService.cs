using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Entities.Admin;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public class ApplicationService : IApplicationService
    {
        protected IMapper Mapper { get; set; }
        protected IDIMDBEntities Context { get; set; }

        public ApplicationService(IMapper mapper)
        {
            Mapper = mapper;
            Context = new IDIMDBEntities();
        }

        public async Task<IList<ApplicationVm>> GetAllAsync()
        {
            var list = await Context.Applications.ToListAsync();

            return Mapper.Map<IList<ApplicationVm>>(list);
        }

        public async Task<ApplicationVm> GetByIdAsync(int id)
        {
            var entity = await Context.Applications.FindAsync(id);

            return Mapper.Map<ApplicationVm>(entity);
        }

        public async Task<IList<ApplicationVm>> GetByUserIdAsync(int userId)
        {
            //var list = await Context.Applications.Where(e => e.UserId == userId)
            //    .Select(e => e.Application)
            //    .ToListAsync();

            var list = await Context.Applications
                .ToListAsync();

            return Mapper.Map<IList<ApplicationVm>>(list);
        }

        public async Task<ApplicationVm> InsertAsync(ApplicationVm model)
        {
            var existing = await Context.Applications.FirstOrDefaultAsync(m => m.ApplicationName == model.ApplicationName);
            if (existing != null)
                throw new ArgumentException($"{nameof(model.ApplicationName)} already exists ");

            var entity = Mapper.Map<Application>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Applications.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<ApplicationVm>(added);
        }

        public async Task<ApplicationVm> UpdateAsync(ApplicationVm model)
        {
            var existing = await Context.Applications.FindAsync(model.ApplicationId);

            if (existing == null)
                throw new ArgumentException($"Application does not exists.");

            var duplicate = await Context.Applications.Where(e => e.ApplicationId != model.ApplicationId).FirstOrDefaultAsync(e => e.ApplicationName == model.ApplicationName);

            if (duplicate != null)
                throw new ArgumentException($"Name already exists.");

            existing.ApplicationName = model.ApplicationName;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;

            await Context.SaveChangesAsync();

            return Mapper.Map<ApplicationVm>(existing);
        }

        public async Task<ApplicationVm> DeleteAsync(int id)
        {
            var existing = await Context.Applications.FindAsync(id);

            if (existing == null)
                throw new ArgumentException($"Application does not exists.");

            Context.Entry(existing).State = EntityState.Deleted;
            await Context.SaveChangesAsync();

            return Mapper.Map<ApplicationVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0)
        {
            var applications = await Context.Applications.ToListAsync();

            return applications.Select(e => new SelectListItem
            {
                Text = e.ApplicationName,
                Value = e.ApplicationId.ToString(),
                Selected = e.ApplicationId == selected
            });
        }
    }
}