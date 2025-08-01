﻿using IDIMWorkBranchProject.Models.Dashboard;
using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardVm> GetAll();
        Task<List<ProjectVm>> ProjectList();
    }
}
