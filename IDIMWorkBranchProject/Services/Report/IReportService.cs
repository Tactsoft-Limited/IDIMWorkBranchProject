using BGB.Data.SqlViews.Wbpm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Report{
	public interface IReportService
	{
		Task<ViewADPReceivePayment> GetADPReceivePaymenAsync(int id);
	}
}

