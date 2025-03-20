using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ContractAgreementService : BaseService<ContractAgreement>, IContractAgreementService
    {
        public ContractAgreementService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<ContractAgreement> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ContractAgreements.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(ContractAgreementSearchVm model)
        {
            var query = _context.ContractAgreements.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.AgreementDate.ToString().Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ContractAgreementId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ContractAgreementVm
                {
                    ContractAgreementId = x.ContractAgreementId,
                    ProjectWorkId = x.ProjectWorkId,
                    ProjectWorkTitleB = x.ProjectWork.ProjectWorkTitleB,
                    ConstructionFirm = x.ConstructionCompany.FirmNameB,
                    AgreementDate = x.AgreementDate,
                    ScanDocument = x.ScanDocument,
                })
            };

            return result;
        }
    }
}