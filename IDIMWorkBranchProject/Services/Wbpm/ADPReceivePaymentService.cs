using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ADPReceivePaymentService : BaseService<ADPReceivePayment>, IADPReceivePaymentService
    {

        public ADPReceivePaymentService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<ADPReceivePayment>> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ADPReceivePayments.Where(x => x.ProjectWorkId == id).ToListAsync();
        }

        public async Task<object> GetPagedAsync(ADPReceivePaymentSearchVm model)
        {
            var query = _context.ADPReceivePayments.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.ProjectWork.ProjectWorkTitle.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ADPReceivePaymentId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ADPReceivePaymentVm
                {
                    ADPReceivePaymentId = x.ADPReceivePaymentId,
                    ProjectWorkId = x.ProjectWorkId,
                    ProjectWorkTitle = x.ProjectWork.ProjectWorkTitle,
                    ProjectWorkTitleB = x.ProjectWork.ProjectWorkTitleB,
                    BillNumber = x.BillNumber,
                    BillDate = x.BillDate,
                    BillPaidPer = x.BillPaidPer,
                    BillPaidAmount = x.BillPaidAmount,
                })
            };

            return result;
        }
    }
}