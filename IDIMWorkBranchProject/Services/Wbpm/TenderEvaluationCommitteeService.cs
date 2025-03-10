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
    public class TenderEvaluationCommitteeService : BaseService<TenderEvaluationCommittee>, ITenderEvaluationCommitteeService
    {
        public TenderEvaluationCommitteeService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<TenderEvaluationCommittee> GetByAdpProjectIdAsync(int id)
        {
            return await _context.TenderEvaluationCommittees.Where(x => x.ADPProjectId == id).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(TenderEvaluationCommitteeSearchVm model)
        {
            var query = _context.TenderEvaluationCommittees.AsQueryable();

            if(model.SearchValue != null)
                query = query.Where(x => x.RecruitmentCommitteeAddDG.NameB.Contains(model.SearchValue) ||
            x.RecruitmentCommitteeDDG.NameB.Contains(model.SearchValue) ||
            x.RecruitmentCommitteeProjectDirector.NameB.Contains(model.SearchValue) ||
            x.RecruitmentCommitteeDirector.NameB.Contains(model.SearchValue));


            //var query = _context.TenderEvaluationCommittees.Where(x =>
            //(string.IsNullOrEmpty(model.SearchValue) || 
            //x.RecruitmentCommitteeAddDG.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeDDG.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeProjectDirector.NameB.Contains(model.SearchValue) ||
            //x.RecruitmentCommitteeDirector.NameB.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
                ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.TenderEvaluationCommitteeId);
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
                data = pagedData.Select(x => new TenderEvaluationCommitteeVm
                {
                    TenderEvaluationCommitteeId = x.TenderEvaluationCommitteeId,
                    ADPProjectId = x.ADPProjectId,
                    ADPProjectTitle = x.ADPProject.ProjectTitle,
                    AddDGName = $"{x.RecruitmentCommitteeAddDG.DesignationB} {x.RecruitmentCommitteeAddDG.NameB}",
                    DDGName = $"{x.RecruitmentCommitteeDDG.DesignationB} {x.RecruitmentCommitteeDDG.NameB}",
                    ProjectDirectorName = $"{x.RecruitmentCommitteeProjectDirector.DesignationB} {x.RecruitmentCommitteeProjectDirector.NameB}",
                    DirectorName = $"{x.RecruitmentCommitteeProjectDirector.DesignationB} {x.RecruitmentCommitteeProjectDirector.NameB}",
                    
                })
            };
            return result;
        }
    }
}