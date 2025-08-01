﻿using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFiscalYearExpenseService : IBaseService<FiscalYearExpense>
    {
        Task<List<FiscalYearExpense>> GetAllByProjectId(int id);
    }
}
