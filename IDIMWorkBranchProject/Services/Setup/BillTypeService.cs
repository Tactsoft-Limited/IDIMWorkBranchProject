using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;
using System.Linq;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Data.Database;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class BillTypeService : IBillTypeService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }
        protected int ApplicationId { get; set; }

        public BillTypeService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
            ApplicationId = (int)UserExtention.GetApplicationId();
        }

        public async Task<List<BillTypeVm>> GetAllAsync()
        {
            var list = await Context.BillTypes.ToListAsync();

            return Mapper.Map<List<BillTypeVm>>(list);
        }

        public async Task<BillTypeVm> GetByIdAsync(int id)
        {
            var entity = await Context.BillTypes
                .FirstOrDefaultAsync(x => x.BillTypeId == id);

            return Mapper.Map<BillTypeVm>(entity);
        }

        public async Task<BillTypeVm> InsertAsync(BillTypeVm model)
        {
            var existing = await Context.BillTypes
                .FirstOrDefaultAsync(e => e.BillTypeName == model.BillTypeName);
            if (existing != null)
                throw new ArgumentException($"Name already exists.");

            var entity = Mapper.Map<BillType>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.BillTypes.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<BillTypeVm>(added);
        }

        public async Task<BillTypeVm> UpdateAsync(BillTypeVm model)
        {
            var existing = await Context.BillTypes
                .FirstOrDefaultAsync(e => e.BillTypeId == model.BillTypeId);
            if (existing == null)
                throw new ArgumentException($"Product Type does not exists.");

            var duplicate = await Context.BillTypes
                .Where(e => e.BillTypeId != model.BillTypeId)
                .FirstOrDefaultAsync(e => e.BillTypeName == model.BillTypeName);
            if (duplicate != null)
                throw new ArgumentException($"Name already exists.");

            existing.BillTypeName = model.BillTypeName;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<BillTypeVm>(existing);
        }

        public async Task<BillTypeVm> DeleteAsync(int id)
        {
            var existing = await Context.BillTypes
                .FirstOrDefaultAsync(e => e.BillTypeId == id);
            if (existing == null)
                throw new ArgumentException($"Product Type does not exists.");

            Context.BillTypes.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<BillTypeVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var billTypes = await Context.BillTypes.ToListAsync();

            return billTypes.Select(e => new SelectListItem
            {
                Text = e.BillTypeName,
                Value = e.BillTypeId.ToString(),
                Selected = e.BillTypeId == selected
            });
        }
    }
}