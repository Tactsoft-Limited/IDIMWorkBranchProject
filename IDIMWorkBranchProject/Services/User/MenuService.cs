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
    public class MenuService : IMenuService
    {
        protected IMapper Mapper { get; set; }
        protected IDIMDBEntities Context { get; set; }

        public MenuService(IMapper mapper)
        {
            Mapper = mapper;
            Context = new IDIMDBEntities();
        }

        public async Task<IList<MenuVm>> GetAllAsync()
        {
            var list = await Context.Menus.ToListAsync();

            return Mapper.Map<IList<MenuVm>>(list);
        }

        public async Task<IList<MenuVm>> GetByApplicationIdAsync(int applicationId)
        {
            var entity = await Context.Menus.Where(e => e.ApplicationId == applicationId).ToListAsync();

            return Mapper.Map<IList<MenuVm>>(entity);
        }

        public async Task<MenuVm> GetByIdAsync(int id)
        {
            var entity = await Context.Menus.FindAsync(id);

            return Mapper.Map<MenuVm>(entity);
        }


        public async Task<MenuVm> InsertAsync(MenuVm model)
        {
            var existing = await Context.Menus.FirstOrDefaultAsync(m => m.ControllerName == model.ControllerName && m.ApplicationId == model.ApplicationId);
            if (existing != null)
                throw new ArgumentException("Controller already exists for this Application.");

            var entity = Mapper.Map<Menu>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Menus.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<MenuVm>(added);
        }

        public async Task<MenuVm> UpdateAsync(MenuVm model)
        {
            var existing = await Context.Menus.FindAsync(model.MenuId);

            if (existing == null)
                throw new ArgumentException("Contoller does not exists.");

            var duplicate = await Context.Menus.Where(e => e.MenuId != model.MenuId).FirstOrDefaultAsync(e => e.ControllerName == model.ControllerName && e.ApplicationId == model.ApplicationId);

            if (duplicate != null)
                throw new ArgumentException("Controller already exists for this Application.");

            existing.Title = model.Title;
            existing.ControllerName = model.ControllerName;
            existing.ApplicationId = model.ApplicationId;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;

            await Context.SaveChangesAsync();

            return Mapper.Map<MenuVm>(existing);
        }

        public async Task<MenuVm> DeleteAsync(int id)
        {
            var existing = await Context.Menus.FindAsync(id);

            if (existing == null)
                throw new ArgumentException("Controller does not exists.");

            Context.Entry(existing).State = EntityState.Deleted;
            await Context.SaveChangesAsync();

            return Mapper.Map<MenuVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0)
        {
            var menus = await Context.Menus.ToListAsync();

            return menus.Select(e => new SelectListItem
            {
                Text = e.ControllerName,
                Value = e.MenuId.ToString(),
                Selected = e.MenuId == selected
            });
        }

        public async Task<IList<MenuGroupByVm>> GetAllGroupByApplicationAsync()
        {
            var groupList = from m in Context.Menus
                            join a in Context.Applications on m.ApplicationId equals a.ApplicationId
                            group m by new { a.ApplicationName } into g
                            select new MenuGroupByVm
                            {
                                ApplicationName = g.Key.ApplicationName,
                                Menus = g.Select(e => new MenuAssignVm
                                {
                                    Menu = new MenuVm
                                    {
                                        MenuId = e.MenuId,
                                        Title = e.Title,
                                        ControllerName = e.ControllerName,
                                        ApplicationId = e.ApplicationId
                                    }
                                }).ToList()
                            };


            return await groupList.ToListAsync();
        }
    }
}