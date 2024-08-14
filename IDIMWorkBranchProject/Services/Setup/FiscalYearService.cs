using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Budget;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class FiscalYearService : IFiscalYearService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public FiscalYearService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<List<FiscalYearVm>> GetAllAsync()
        {
            var list = await Context.FiscalYears.ToListAsync();
            list = list.OrderByDescending(e => e.FiscalYearId).ToList();

            return Mapper.Map<List<FiscalYearVm>>(list);
        }

        public async Task<FiscalYearVm> GetByIdAsync(int id)
        {
            var entity = await Context.FiscalYears.FindAsync(id);

            return Mapper.Map<FiscalYearVm>(entity);
        }


        public async Task<FiscalYearVm> InsertAsync(FiscalYearVm model)
        {
            var existing = await Context.FiscalYears.FirstOrDefaultAsync(m => m.FiscalYearDescription == model.FiscalYearDescription);
            if (existing != null)
                throw new ArgumentException("Fiscal Year already exists ");

            var entity = Mapper.Map<FiscalYear>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.FiscalYears.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<FiscalYearVm>(added);
        }

        public async Task<FiscalYearVm> UpdateAsync(FiscalYearVm model)
        {
            var existing = await Context.FiscalYears.FindAsync(model.FiscalYearId);
            if (existing == null)
                throw new ArgumentException("Fiscal Year not found.");

            var duplicate = await Context.FiscalYears
                .Where(e => e.FiscalYearId != model.FiscalYearId)
                .FirstOrDefaultAsync(e => e.FiscalYearDescription == model.FiscalYearDescription);
            if (duplicate != null)
                throw new ArgumentException("Fiscal Year already exists ");

            existing.FiscalYearDescription = model.FiscalYearDescription;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;

            await Context.SaveChangesAsync();

            return Mapper.Map<FiscalYearVm>(existing);
        }

        public async Task<FiscalYearVm> DeleteAsync(int id)
        {
            var existing = await Context.FiscalYears.FindAsync(id);

            if (existing == null)
                throw new ArgumentException("Fiscal Year not found.");

            Context.FiscalYears.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<FiscalYearVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var fiscalYears = await Context.FiscalYears
                .OrderBy(e => e.StartDate)
                .ToListAsync();

            if (selected == 0)
                selected = fiscalYears
                    .Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
                    .Select(e => e.FiscalYearId).FirstOrDefault();

            return fiscalYears.Select(e => new SelectListItem
            {
                Text = e.FiscalYearDescription,
                Value = e.FiscalYearId.ToString(),
                Selected = e.FiscalYearId == selected
            });
        }
    }
}