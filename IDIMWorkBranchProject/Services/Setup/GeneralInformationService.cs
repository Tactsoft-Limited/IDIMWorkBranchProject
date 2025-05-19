using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.Collections.Select2;

namespace IDIMWorkBranchProject.Services.Setup
{
    public class GeneralInformationService : IGeneralInformationService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public GeneralInformationService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var regiments = await Context.GeneralInformations.Where(e => e.ArmyId == selected).ToListAsync();

            return regiments.Select(e => new SelectListItem
            {
                Text = e.RegimentNo,
                Value = e.ArmyId.ToString(),
                Selected = e.ArmyId == selected
            });
        }

        public async Task<Select2PagedResult> GetBySelect2Async(Select2Param param)
        {
            var select2 = new Select2PagedResult();

            var query = Context.GeneralInformations.AsQueryable();

            if (!string.IsNullOrEmpty(param.Term))
                query = query.Where(e => e.RegimentNo.Contains(param.Term));

            var list = await query.OrderBy(e => e.RegimentNo).Take(20).ToListAsync();

            select2.Results = list.Select(e => new Select2Result
            {
                id = e.ArmyId.ToString(),
                text = e.RegimentNo
            }).ToList();

            return select2;
        }
    }
}