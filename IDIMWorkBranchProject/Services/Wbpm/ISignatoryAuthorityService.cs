﻿using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface ISignatoryAuthorityService : IBaseService<SignatoryAuthority>
    {
       // Task<object> GetByAsync(SignatoryAuthoritySearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
