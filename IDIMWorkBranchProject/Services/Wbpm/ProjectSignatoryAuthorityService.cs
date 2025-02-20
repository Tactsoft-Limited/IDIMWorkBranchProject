using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectSignatoryAuthorityService : BaseService<ProjectSignatoryAuthority>, IProjectSignatoryAuthorityService 
    {
        public ProjectSignatoryAuthorityService(IDIMDBEntities context) : base(context)
        {
        }


        public async Task<ProjectSignatoryAuthority> GetByAdpProjectIdAsync(int id)
        {
            return await _context.ProjectSignatoryAuthority.Where(x => x.ADPProjectId == id).FirstOrDefaultAsync();
        }


        public async Task<object> GetPagedAsync(ProjectSignatoryAuthoritySearchVM model)
        {
            var query = _context.ProjectSignatoryAuthority.AsQueryable();

            if (model.SearchValue != null)
                query = query.Where(x => x.signatureAuthorityHeadAssistant.AuthorityNameB.Contains(model.SearchValue) ||
            x.signatureAuthorityConcernedEngineer.AuthorityNameB.Contains(model.SearchValue) ||
            x.signatureAuthoritySectionICT.AuthorityNameB.Contains(model.SearchValue) ||
            x.signatureAuthorityBranchClerk.AuthorityNameB.Contains(model.SearchValue));


            //var query = _context.TenderEvaluationCommittees.Where(x =>
            //(string.IsNullOrEmpty(model.SearchValue) || 
            //x.RecruitmentCommitteeAddDG.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeDDG.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeProjectDirector.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeDirector.NameB.Contains(model.SearchValue)));

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
                    HeadAssistantName = $"{x.signatureAuthorityHeadAssistant.DesignationB} {x.signatureAuthorityHeadAssistant.AuthorityNameB}",
                    ConcernedEngineerName = $"{x.signatureAuthorityConcernedEngineer.DesignationB} {x.signatureAuthorityConcernedEngineer.AuthorityNameB}",
                    SectionICName = $"{x.signatureAuthoritySectionICT.DesignationB} {x.signatureAuthoritySectionICT.AuthorityNameB}",
                    BranchClerkName = $"{x.signatureAuthorityBranchClerk.DesignationB} {x.signatureAuthorityBranchClerk.AuthorityNameB}",

                })
            };
            return result;
        }
    }
}