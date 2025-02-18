using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;


namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface ITenderEvaluationCommitteeService : IBaseService<TenderEvaluationCommittee>
    {
        Task<TenderEvaluationCommittee> GetByAdpProjectIdAsync(int id);
        Task<object> GetPagedAsync(TenderEvaluationCommitteeSearchVm model);
    }
}
