using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.Setup;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Data.Database;

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
        public async Task<object> GetAllAsync(ConstructionFirmSearchVm model)
        {
            if (model == null)
                model = new ConstructionFirmSearchVm();

            // Build the query with the additional filters
            var query = Context.ConstructionFirms.Where(x =>
                    (string.IsNullOrEmpty(model.ConstructionFirmName) || x.ConstructionFirmName.Contains(model.ConstructionFirmName)) &&
                    (string.IsNullOrEmpty(model.ContactPerson) || x.ContactPerson.Contains(model.ContactPerson)) &&
                    (string.IsNullOrEmpty(model.ContactPhone) || x.ContactPhone.Contains(model.ContactPhone)) &&
                    (string.IsNullOrEmpty(model.Email) || x.Email.Contains(model.Email)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
                ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ConstructionFirmId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize)
                                              .ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ConstructionFirmVm
                {
                    ConstructionFirmId = x.ConstructionFirmId,
                    ConstructionFirmName = x.ConstructionFirmName,
                    ContactPerson = x.ContactPerson,
                    ContactPhone = x.ContactPhone,
                    Email = x.Email,
                    Address = x.Address,
                })
            };

            return result;
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
            existing.ContactPerson = model.ContactPerson;
            existing.Email = model.Email;
            existing.ContactPhone = model.ContactPhone;
            existing.Address = model.Address;

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

        public async Task<IEnumerable<SelectListItem>> GetDropdownBySubProjectAsync(int subProjectId)
        {
            return await (from cf in Context.ConstructionFirms
                          join sp in Context.SubProjects on cf.ConstructionFirmId equals sp.ConstructionFirmId
                          where sp.SubProjectId == subProjectId
                          select new SelectListItem
                          {
                              Text = cf.ConstructionFirmName,  // Text for the dropdown
                              Value = cf.ConstructionFirmId.ToString()  // Value for the dropdown item
                          }).Distinct().ToListAsync();
        }


    }
}