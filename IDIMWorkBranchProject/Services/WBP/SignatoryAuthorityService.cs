using AutoMapper;
using BGB.Data.Entities.Pm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Extentions;
using System.Data.Entity;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Data.Database;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class SignatoryAuthorityService : ISignatoryAuthorityService
    {
        protected IDIMDBEntities _context;
        protected IMapper _mapper;

        public SignatoryAuthorityService(IMapper mapper)
        {
            _context = new IDIMDBEntities();
            _mapper = mapper;
        }

        private IQueryable<SignatoryAuthority> GetAll()
        {
            return _context.SignatoryAuthorities.OrderByDescending(e => e.SignatoryAuthorityId).AsQueryable();
        }

        public async Task<List<SignatoryAuthorityVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return _mapper.Map<List<SignatoryAuthorityVm>>(list);
        }

        public async Task<SignatoryAuthorityVm> GetByIdAsync(int id)
        {
            var entity = await _context.SignatoryAuthorities.FindAsync(id);

            return _mapper.Map<SignatoryAuthorityVm>(entity);
        }


        public async Task<SignatoryAuthorityVm> InsertAsync(SignatoryAuthorityVm model)
        {
            var entity = _mapper.Map<SignatoryAuthority>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = _context.SignatoryAuthorities.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<SignatoryAuthorityVm>(added);
        }

        public async Task<SignatoryAuthorityVm> UpdateAsync(SignatoryAuthorityVm model)
        {
            var existing = await _context.SignatoryAuthorities
                .Where(e => e.SignatoryAuthorityId == model.SignatoryAuthorityId)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"SignatoryAuthority does not exists.");

            existing.ProjectId = model.ProjectId;
            existing.ReceivePaymentId = model.ReceivePaymentId;
            existing.AuthorityName = model.AuthorityName;
            existing.Designation = model.Designation;
            existing.Note = model.Note;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await _context.SaveChangesAsync();

            return _mapper.Map<SignatoryAuthorityVm>(existing);
        }

        public async Task<SignatoryAuthorityVm> DeleteAsync(int id)
        {
            var existing = await _context.SignatoryAuthorities
                .Where(e => e.SignatoryAuthorityId == id)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new ArgumentException($"SignatoryAuthority does not exists.");

            _context.SignatoryAuthorities.Remove(existing);
            await _context.SaveChangesAsync();

            return _mapper.Map<SignatoryAuthorityVm>(existing);
        }

        public async Task<object> GetByAsync(SignatoryAuthoritySearchVm filter = null)
        {
            if (filter == null)
                filter = new SignatoryAuthoritySearchVm();

            // Querying SubProjects and including related Project
            var data = _context.SignatoryAuthorities.Include(x => x.Project).Include(x => x.ReceivePayment).AsQueryable();

            var query = data.Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                    (string.IsNullOrEmpty(filter.LetterNumber) || x.ReceivePayment.LetterNo.Contains(filter.LetterNumber)) &&
                    (!filter.ProjectId.HasValue || x.ProjectId == filter.ProjectId) &&
                    (!filter.ReceivePaymentId.HasValue || x.ReceivePaymentId == filter.ReceivePaymentId));

            // Sorting: Dynamically apply sorting based on DataTable's request
            query = !string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection)
               ? query.OrderBy($"{filter.SortColumn} {filter.SortDirection}")
               : query.OrderBy(x => x.ReceivePaymentId);  // Default ordering by SubProjectId

            // Pagination: Apply pagination based on DataTable's filter
            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(filter.PageIndex * filter.PageSize)
                                              .Take(filter.PageSize)
                                              .ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = filter.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new SignatoryAuthorityVm
                {
                    SignatoryAuthorityId = x.SignatoryAuthorityId,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.ProjectName,
                    ReceivePaymentId = x.ReceivePaymentId,
                    LetterNumber = x.ReceivePayment.LetterNo,
                    AuthorityName = x.AuthorityName,
                    Designation = x.Designation,
                    Note = x.Note,
                })
            };

            return result;
        }


        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var projects = await _context.SignatoryAuthorities.ToListAsync();

            return projects.Select(e => new SelectListItem
            {
                Text = e.AuthorityName,
                Value = e.SignatoryAuthorityId.ToString(),
                Selected = e.SignatoryAuthorityId == selected
            });
        }
    }
}