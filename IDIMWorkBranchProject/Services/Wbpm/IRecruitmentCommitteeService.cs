using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRecruitmentCommitteeService : IBaseService<RecruitmentCommittee>
    {
        Task<object> GetPagedAsync(RecruitmentCommitteeSearchVm model);
        Task<IEnumerable<SelectListItem>> DropdownAsync(int? selected = 0);
    }
}