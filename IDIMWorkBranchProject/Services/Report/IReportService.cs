using BGB.Data.SqlViews.Wbpm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Report
{
    public interface IReportService
    {
        Task<List<ViewADPReceivePayment>> GetADPReceivePaymenAsync(int id);
    }
}

