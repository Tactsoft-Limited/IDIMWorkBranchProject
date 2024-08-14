using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class ConstructionFirmService : IConstructionFirmService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }
        protected int ApplicationId { get; set; }

        public ConstructionFirmService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
            ApplicationId = DefaultData.ApplicationId;
        }

        public async Task<List<ConstructionFirmVm>> GetAllAsync()
        {
            var list = await Context.ConstructionFirms.ToListAsync();

            return Mapper.Map<List<ConstructionFirmVm>>(list);
        }

        public async Task<ConstructionFirmVm> GetByIdAsync(int id)
        {
            var entity = await Context.ConstructionFirms
                .FirstOrDefaultAsync(x => x.ConstructionFirmId == id);

            return Mapper.Map<ConstructionFirmVm>(entity);
        }

        public async Task<ConstructionFirmVm> InsertAsync(ConstructionFirmVm model)
        {
            var existing = await Context.ConstructionFirms
                .FirstOrDefaultAsync(m => m.ConstructionFirmName == model.ConstructionFirmName);
            if (existing != null)
                throw new ArgumentException($"Name already exists.");

            var entity = Mapper.Map<ConstructionFirm>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ConstructionFirms.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<ConstructionFirmVm>(added);
        }

        public async Task<ConstructionFirmVm> UpdateAsync(ConstructionFirmVm model)
        {
            var existing = await Context.ConstructionFirms
                .FirstOrDefaultAsync(e => e.ConstructionFirmId == model.ConstructionFirmId);
            if (existing == null)
                throw new ArgumentException($"Construction Firm does not exists.");

            var duplicateName = await Context.ConstructionFirms
                .Where(x => x.ConstructionFirmId != model.ConstructionFirmId)
                .FirstOrDefaultAsync(m => m.ConstructionFirmName == model.ConstructionFirmName);
            if (duplicateName != null)
                throw new ArgumentException($"Name already exists.");

            existing.ConstructionFirmName = model.ConstructionFirmName;
            existing.Address = model.Address;
            existing.ContactPerson = model.ContactPerson;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ConstructionFirmVm>(existing);
        }

        public async Task<ConstructionFirmVm> DeleteAsync(int id)
        {
            var existing = await Context.ConstructionFirms
                .FirstOrDefaultAsync(x => x.ConstructionFirmId == id);
            if (existing == null)
                throw new ArgumentException($"Construction Firm does not exists.");

            Context.Entry(existing).State = EntityState.Deleted;
            await Context.SaveChangesAsync();

            return Mapper.Map<ConstructionFirmVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var list = await Context.ConstructionFirms.ToListAsync();

            return list.Select(e => new SelectListItem
            {
                Text = e.ConstructionFirmName,
                Value = e.ConstructionFirmId.ToString(),
                Selected = e.ConstructionFirmId == selected
            });
        }
    }
}