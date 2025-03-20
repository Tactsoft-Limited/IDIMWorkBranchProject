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
    public class BGBMiscellaneousFundService : BaseService<BGBMiscellaneousFund>, IBGBMiscellaneousFundService
    {
        public BGBMiscellaneousFundService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<BGBMiscellaneousFund> GetByADPPaymentReceiveIdAsync(int id)
        {
            return await _context.BGBMiscellaneousFunds.Where(x => x.ADPReceivePaymentId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BGBMiscellaneousFund>> GetByProjectWorkIdAsync(int projectWorkId)
        {
            return await _context.BGBMiscellaneousFunds.Where(x => x.ProjectWorkId == projectWorkId).ToListAsync();
        }

        public async Task<object> GetPagedAsync(BGBMiscellaneousFundSearchVm model)
        {
            var query = _context.BGBMiscellaneousFunds.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.ProjectWork.ProjectWorkTitleB.Contains(model.SearchValue) ||
            x.BankName.Contains(model.SearchValue) ||
            x.BrunchName.Contains(model.SearchValue) ||
            x.AccountName.Contains(model.SearchValue) ||
            x.AccountNumber.Contains(model.SearchValue) ||
            x.LetterNo.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.FundId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new BGBMiscellaneousFundVm
                {
                    FundId = x.FundId,
                    ADPReceivePaymentId = x.ADPReceivePaymentId,
                    ProjectWorkId = x.ProjectWorkId,
                    ProjectWorkTitle = x.ProjectWork.ProjectWorkTitle,
                    ProjectWorkTitleB = x.ProjectWork.ProjectWorkTitleB,
                    LetterNo = x.LetterNo,
                    DepositeDate = x.DepositeDate,
                    PayOrderNo = x.PayOrderNo,
                    PayOrderDate = x.PayOrderDate,
                    BankName = x.BankName,
                    BrunchName = x.BrunchName,
                    AccountName = x.AccountName,
                    AccountNumber = x.AccountNumber,
                    Amount = x.Amount,
                })
            };

            return result;
        }
    }
}