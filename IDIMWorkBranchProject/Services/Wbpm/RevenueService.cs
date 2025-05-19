using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueService : BaseService<Revenue>, IRevenueService
    {
        public RevenueService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<object> GetPagedAsync(RevenueSearchVm model)
        {
            var query = _context.Revenues.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.WorkTitle.Contains(model.SearchValue) || x.EstimateCost.ToString().Contains(model.SearchValue)) || x.FisCalYearId.ToString().Contains(model.SearchValue));


            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.RevenueId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new RevenueVm
                {
                    RevenueId = x.RevenueId,                    
                    WorkTitleB = x.WorkTitleB,
                    EstimateCost = x.EstimateCost,     
                    Remarks = x.Remarks,
                })
                
            };

            return result;
        }

        public async Task<string> GetWorkTitle(int? revenueId)
        {
            return await _context.Revenues.Where(x => x.RevenueId == revenueId).Select(x => x.WorkTitleB).FirstOrDefaultAsync();
        }
    }
}