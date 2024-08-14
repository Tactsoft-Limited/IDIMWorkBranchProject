using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Irms;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class UnitService : IUnitService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public UnitService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }
        public async Task<List<UnitVm>> GetAllAsync()
        {
            var list = await Context.SetupUnits.ToListAsync();

            return Mapper.Map<List<UnitVm>>(list);
        }

        public async Task<UnitVm> GetByIdAsync(int id)
        {
            var entity = await Context.SetupUnits.FindAsync(id);

            return Mapper.Map<UnitVm>(entity);
        }

        public async Task<UnitVm> InsertAsync(UnitVm model)
        {
            var existing = await Context.SetupUnits.Where(e => e.UnitName == model.UnitName).FirstOrDefaultAsync();

            if (existing != null)
                throw new ArgumentException($"Name already exists.");

            var entity = Mapper.Map<IrmsSetupUnit>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.SetupUnits.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<UnitVm>(added);
        }

        public async Task<UnitVm> UpdateAsync(UnitVm model)
        {
            var existing = await Context.SetupUnits.Where(e => e.UnitId == model.UnitId).FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"Unit does not exists.");

            var duplicate = await Context.SetupUnits
                .Where(e => e.UnitId != model.UnitId)
                .FirstOrDefaultAsync(e => e.UnitName == model.UnitName);

            if (duplicate != null)
                throw new ArgumentException($"Name already exists ");

            existing.UnitName = model.UnitName;
            existing.UnitNameB = model.UnitNameB;
            existing.CoreId = model.CoreId;
            // existing.PlaceId = model.PlaceId;
            existing.Remark = model.Remark;
            existing.Israb = model.Israb;
            existing.SectorId = model.SectorId;
            existing.RegionId = model.RegionId;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;

            await Context.SaveChangesAsync();

            return Mapper.Map<UnitVm>(existing);
        }

        public async Task<UnitVm> DeleteAsync(int id)
        {
            var existing = await Context.SetupUnits.Where(e => e.UnitId == id).FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"Unit does not exists.");

            Context.SetupUnits.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<UnitVm>(existing);
        }

        public async Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0)
        {
            var units = await Context.SetupUnits.ToListAsync();

            return units.Select(e => new SelectListItem
            {
                Text = e.UnitName,
                Value = e.UnitId.ToString(),
                Selected = e.UnitId == selected
            });
        }

        //public async Task<SelectList> GetSelectAsync(int? selected = 0)
        //{
        //    var units = await Context.SetupUnits.Select(e=>new
        //    {
        //        e.UnitId,
        //        e.UnitName,
        //        e.SetupRegion.RegionName
        //    }).ToListAsync();
        //    selected = units.FirstOrDefault(e => e.UnitId == selected)?.UnitId;
        //    var data = new SelectList(units, "UnitId", "UnitName", "RegionName", selected);
        //    return new SelectList(units, "UnitId", "UnitName", "RegionName", selected);
        //}
    }
}