using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using System.Data.Entity;
using IDIMWorkBranchProject.Data.Database;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class SignatoryAuthorityService : BaseService<SignatoryAuthority>, ISignatoryAuthorityService
    {
        public SignatoryAuthorityService(IDIMDBEntities context) : base(context)
        {
        }

        //public async Task<object> GetByAsync(SignatoryAuthoritySearchVm filter = null)
        //{
        //    if (filter == null)
        //        filter = new SignatoryAuthoritySearchVm();

        //    // Querying SubProjects and including related Project
        //    var data = _context.SignatoryAuthorities.Include(x => x.Project).Include(x => x.ReceivePayment).AsQueryable();

        //    var query = data.Where(x =>
        //            (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
        //            (string.IsNullOrEmpty(filter.LetterNumber) || x.ReceivePayment.LetterNo.Contains(filter.LetterNumber)) &&
        //            (!filter.ProjectId.HasValue || x.ProjectId == filter.ProjectId) &&
        //            (!filter.ReceivePaymentId.HasValue || x.ReceivePaymentId == filter.ReceivePaymentId));

        //    // Sorting: Dynamically apply sorting based on DataTable's request
        //    query = !string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection)
        //       ? query.OrderBy($"{filter.SortColumn} {filter.SortDirection}")
        //       : query.OrderBy(x => x.ReceivePaymentId);  // Default ordering by SubProjectId

        //    // Pagination: Apply pagination based on DataTable's filter
        //    var totalRecords = await query.CountAsync();
        //    var filteredRecords = await query.CountAsync();
        //    var pagedData = await query.Skip(filter.PageIndex * filter.PageSize)
        //                                      .Take(filter.PageSize)
        //                                      .ToListAsync();

        //    // Return the response in DataTables format
        //    var result = new
        //    {
        //        draw = filter.Draw,
        //        recordsTotal = totalRecords,
        //        recordsFiltered = filteredRecords,
        //        data = pagedData.Select(x => new SignatoryAuthorityVm
        //        {
        //            SignatoryAuthorityId = x.SignatoryAuthorityId,
        //            ProjectId = x.ProjectId,
        //            ProjectName = x.Project.ProjectName,
        //            ReceivePaymentId = x.ReceivePaymentId,
        //            LetterNumber = x.ReceivePayment.LetterNo,
        //            AuthorityName = x.AuthorityName,
        //            Designation = x.Designation,
        //            Note = x.Note,
        //        })
        //    };

        //    return result;
        //}


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