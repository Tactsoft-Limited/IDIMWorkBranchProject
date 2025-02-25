using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class VatTaxCollateralService : BaseService<VatTaxCollateral>, IVatTaxCollateralService
    {
        public VatTaxCollateralService(IDIMDBEntities context) : base(context)
        {
        }
    }
}