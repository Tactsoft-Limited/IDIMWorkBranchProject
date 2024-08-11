using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using IDIMWorkBranchProject.Entity;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class QuarterService : IQuarterService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }
        public QuarterService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<List<QuarterVm>> GetAllAsync()
        {
            var list = await Context.Quarters.ToListAsync();
            list = list.OrderByDescending(e => e.QuarterId).ToList();

            return Mapper.Map<List<QuarterVm>>(list);
        }

        public async Task<QuarterVm> GetByIdAsync(int id)
        {
            var entity = await Context.Quarters.FindAsync(id);

            return Mapper.Map<QuarterVm>(entity);
        }


        public async Task<QuarterVm> InsertAsync(QuarterVm model)
        {
            var existing = await Context.Quarters.FirstOrDefaultAsync(m => m.QuarterName == model.QuarterName);
            if (existing != null)
                throw new ArgumentException("Quarter already exists ");

            var entity = Mapper.Map<Quarter>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Quarters.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<QuarterVm>(added);
        }

        public async Task<QuarterVm> UpdateAsync(QuarterVm model)
        {
            var existing = await Context.Quarters.FindAsync(model.QuarterId);
            if (existing == null)
                throw new ArgumentException("Quarter not found.");

            var duplicate = await Context.Quarters
                .Where(e => e.QuarterId != model.QuarterId)
                .FirstOrDefaultAsync(e => e.QuarterName == model.QuarterName);
            if (duplicate != null)
                throw new ArgumentException("Quarter already exists ");

            existing.QuarterName = model.QuarterName;
            existing.YearFrom = model.YearFrom;
            existing.YearTo = model.YearTo;
            existing.MonthFrom = model.MonthFrom;
            existing.MonthTo = model.MonthTo;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;

            await Context.SaveChangesAsync();

            return Mapper.Map<QuarterVm>(existing);
        }

        public async Task<QuarterVm> DeleteAsync(int id)
        {
            var existing = await Context.Quarters.FindAsync(id);

            if (existing == null)
                throw new ArgumentException("Quarter not found.");

            Context.Quarters.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<QuarterVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var quarters = await Context.Quarters.OrderByDescending(e => e.YearFrom).ThenByDescending(e => e.MonthFrom).ToListAsync();

            return quarters.Select(e => new SelectListItem
            {
                Text = e.QuarterName,
                Value = e.QuarterId.ToString(),
                Selected = e.QuarterId == selected
            });
        }
    }
}