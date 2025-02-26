using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectSignatoryAuthorityService : BaseService<ProjectSignatoryAuthority>, IProjectSignatoryAuthorityService
    {
        public ProjectSignatoryAuthorityService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<ProjectSignatoryAuthority> GetByAdpProjectIdAsync(int aDPProjectId)
        {
            return await _context.ProjectSignatoryAuthorities.Where(x => x.ADPProjectId == aDPProjectId).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(ProjectSignatoryAuthoritySearchVm model)
        {
            var query = _context.ProjectSignatoryAuthorities.AsQueryable();

            if (model.SearchValue != null)
                query = query.Where(x => x.HeadAssistantSignatoryAuthority.AuthorityNameB.Contains(model.SearchValue) ||
            x.ConcernedEngineerSignatoryAuthority.AuthorityNameB.Contains(model.SearchValue) ||
            x.SectionICTSignatoryAuthority.AuthorityNameB.Contains(model.SearchValue) ||
            x.BranchClerkSignatoryAuthority.AuthorityNameB.Contains(model.SearchValue));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
                ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ProjectSignatoryAuthorityId);
            // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectSignatoryAuthorityVm
                {
                    ProjectSignatoryAuthorityId = x.ProjectSignatoryAuthorityId,
                    ADPProjectId = x.ADPProjectId,
                    ADPProjectTitle = x.ADPProject.ProjectTitle,
                    HeadAssistantName = $"{x.HeadAssistantSignatoryAuthority.DesignationB} {x.HeadAssistantSignatoryAuthority.AuthorityNameB}",
                    ConcernedEngineerName = $"{x.ConcernedEngineerSignatoryAuthority.DesignationB} {x.ConcernedEngineerSignatoryAuthority.AuthorityNameB}",
                    SectionICTName = $"{x.SectionICTSignatoryAuthority.DesignationB} {x.SectionICTSignatoryAuthority.AuthorityNameB}",
                    BranchClerkName = $"{x.BranchClerkSignatoryAuthority.DesignationB} {x.BranchClerkSignatoryAuthority.AuthorityNameB}",

                })
            };
            return result;
        }
    }
}