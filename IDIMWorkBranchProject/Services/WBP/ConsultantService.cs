using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ConsultantService : IConsultantService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ConsultantService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<Consultant> GetAll()
        {
            return Context.Consultants.OrderByDescending(e => e.ConsultantId).AsQueryable();
        }

        public async Task<List<ConsultantVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ConsultantVm>>(list);
        }

        public async Task<ConsultantVm> GetByIdAsync(int id)
        {
            var entity = await Context.Consultants.FindAsync(id);

            return Mapper.Map<ConsultantVm>(entity);
        }


        public async Task<ConsultantVm> InsertAsync(ConsultantVm model)
        {

            var entity = Mapper.Map<Consultant>(model);
            //entity.CreatedDateTime = DateTime.Now;
            //entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Consultants.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultantVm>(added);
        }

        public async Task<ConsultantVm> UpdateAsync(ConsultantVm model)
        {
            var existing = await Context.Consultants.FirstOrDefaultAsync(e => e.ConsultantId == model.ConsultantId);
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.Name = model.Name;
            existing.Address = model.Address;
            existing.Phone = model.Phone;
            existing.Email = model.Email;
            existing.Fax = model.Fax;
            //existing.UpdatedDateTime = DateTime.Now;
            //existing.UpdatedUser = UserExtention.GetUserId();
            //existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultantVm>(existing);
        }

        public async Task<List<ConsultantVm>> GetByAsync(ConsultantSearchVm filter = null)
        {
            if (filter == null)
                filter = new ConsultantSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ConsultantVm>>(list);
        }

        public async Task<ConsultantVm> DeleteAsync(int id)
        {
            var existing = await Context.Consultants.FirstOrDefaultAsync(e => e.ConsultantId == id);
            if (existing == null)
                throw new ArgumentException($"Project Status does not exists.");

            Context.Consultants.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ConsultantVm>(existing);
        }
        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var consultants = await Context.Consultants.ToListAsync();

            return consultants.Select(e => new SelectListItem
            {
                Text = $"{e.ConsultantId} - {e.Name}",
                Value = e.ConsultantId.ToString(),
                Selected = e.ConsultantId == selected
            });
        }
    }
}