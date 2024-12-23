using AutoMapper;
using BGB.Data.Database;
using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ContractorBillPaymentService : IContractorBillPaymentService
    {
        protected readonly IDIMDBEntities _context;
        protected readonly IMapper _mapper;

        public ContractorBillPaymentService(IDIMDBEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ContractorBillPaymentVm> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ContractorBillPaymentVm>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ContractorBillPaymentVm>> GetByAsync(ConsultantSearchVm filter = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ContractorBillPaymentVm> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            throw new System.NotImplementedException();
        }

        public Task<ContractorBillPaymentVm> InsertAsync(ContractorBillPaymentVm model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ContractorBillPaymentVm> UpdateAsync(ContractorBillPaymentVm model)
        {
            throw new System.NotImplementedException();
        }
    }
}