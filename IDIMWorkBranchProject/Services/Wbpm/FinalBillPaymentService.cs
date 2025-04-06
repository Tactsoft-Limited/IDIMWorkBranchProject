using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FinalBillPaymentService : BaseService<FinalBillPayment>, IFinalBillPaymentService
    {
        public FinalBillPaymentService(IDIMDBEntities context) : base(context)
        {
        }
        
    }
}