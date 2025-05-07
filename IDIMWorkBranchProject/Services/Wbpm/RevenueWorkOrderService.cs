using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueWorkOrderService : BaseService<RevenueWorkOrder>, IRevenueWorkOrderService
    {
        public RevenueWorkOrderService(IDIMDBEntities context) : base(context)
        {
        }
    }
}