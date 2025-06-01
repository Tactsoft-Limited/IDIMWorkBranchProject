using AutoMapper;
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
    public class ProjectWorkService : BaseService<ProjectWork>, IProjectWorkService
    {
        private readonly IMapper _mapper;
        public ProjectWorkService(IDIMDBEntities context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ProjectWorkDetailsVm>> GetAllByAdpProjectId(int id)
        {
            var query = from adpProject in _context.ADPProjects
                        join projectWork in _context.ProjectWorks on adpProject.ADPProjectId equals projectWork.ADPProjectId
                        join contractAgreement in _context.ContractAgreements on projectWork.ProjectWorkId equals contractAgreement.ProjectWorkId into contractAgreementJoin
                        from contractAgreement in contractAgreementJoin.DefaultIfEmpty()
                        join constructionCompany in _context.ConstructionCompanies on contractAgreement.ConstructionCompanyId equals constructionCompany.ConstructionCompanyId into constructionCompanyJoin
                        from constructionCompany in constructionCompanyJoin.DefaultIfEmpty()
                        join workOrder in _context.WorkOrders on projectWork.ProjectWorkId equals workOrder.ProjectWorkId into workOrderJoin
                        from workOrder in workOrderJoin.DefaultIfEmpty()
                        join projectWorkStatus in _context.ProjectWorkStatuses on projectWork.ProjectWorkId equals projectWorkStatus.ProjectWorkId into projectWorkStatusJoin
                        from projectWorkStatus in projectWorkStatusJoin.DefaultIfEmpty()
                        join noha in _context.Nohas on projectWork.ProjectWorkId equals noha.ProjectWorkId into nohaJoin
                        from noha in nohaJoin.DefaultIfEmpty()
                        join performanceSecurity in _context.PerformanceSecurities on projectWork.ProjectWorkId equals performanceSecurity.ProjectWorkId into performanceSecurityJoin
                        from performanceSecurity in performanceSecurityJoin.DefaultIfEmpty()
                        where adpProject.ADPProjectId == id  // Filter by Project ID
                        select new ProjectWorkDetailsVm
                        {
                            ADPProjectId = adpProject.ADPProjectId,
                            ProjectTitle = adpProject.ProjectTitle,
                            ProjectWorkId = projectWork.ProjectWorkId,
                            ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
                            ProjectWorkTitle = projectWork.ProjectWorkTitle,
                            EstimatedCost = projectWork.EstimatedCost,
                            IsFurnitureIncluded = projectWork.IsFurnitureIncluded,
                            IsNoahCompleted = projectWork.IsNoahCompleted,
                            IsPerformanceSecuritySubmited = projectWork.IsPerformanceSecuritySubmited,
                            IsAgreementCompleted = projectWork.IsAgreementCompleted,
                            IsWorkOrderCompleted = projectWork.IsWorkOrderCompleted,
                            IsFinalBillSubmitted = projectWork.IsFinalBillSubmitted,                            
                            FirmNameB = constructionCompany.FirmNameB,
                            NohaDate = noha.NohaDate,
                            NohaDocument = noha.ScanDocument,
                            SubmissionDate = performanceSecurity.SubmissionDate,
                            ExpiryDate = performanceSecurity.ExpiryDate,
                            PerformanceSecurityDocument = performanceSecurity.ScanDocument,
                            AgreementDate = contractAgreement.AgreementDate,
                            AgreementDocument = contractAgreement.ScanDocument,
                            WorkOrderDate = workOrder.WorkOrderDate,
                            StartDate = workOrder.StartDate,
                            EndDate = workOrder.EndDate,
                            WorkOrderDocument = workOrder.ScanDocument,
                            ProjectWorkStatus = projectWorkStatus.StatusTypeId,
                        };

            return await query.ToListAsync();

        }

        public async Task<object> GetPagedAsync(ProjectWorkSearchVm model)
        {
            var query = _context.ProjectWorks.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) || x.ProjectWorkTitle.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ProjectWorkId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectWorkVm
                {
                    ADPProjectId = x.ADPProjectId,
                    ProjectTitle = x.ADPProject.ProjectTitle,
                    ProjectWorkTitleB = x.ProjectWorkTitleB,
                    ProjectWorkTitle = x.ProjectWorkTitle,
                    EstimatedCost = x.EstimatedCost,
                    AgreementDate = x.ContractAgreements.FirstOrDefault(ca => ca.ProjectWorkId == x.ProjectWorkId)?.AgreementDate,
                    WorkStartDate = x.WorkOrders.FirstOrDefault(ca => ca.ProjectWorkId == x.ProjectWorkId)?.StartDate,
                    WorkEndDate = x.WorkOrders.FirstOrDefault(ca => ca.ProjectWorkId == x.ProjectWorkId)?.EndDate,
                    BankGuaranteeEndDate = x.PerformanceSecurities.FirstOrDefault(ca => ca.ProjectWorkId == x.ProjectWorkId)?.ExpiryDate,
                    WorkStatus = ((StatusType?)x.ProjectWorkStatuses.FirstOrDefault(pw => pw.ProjectWorkId == x.ProjectWorkId)?.StatusTypeId)?.ToString(),
                    Remarks = x.Remarks,
                })
            };

            return result;
        }

        public async Task<string> GetProjectWorkTitle(int? ProjectWorkId)
        {
            return await _context.ProjectWorks.Where(x => x.ProjectWorkId == ProjectWorkId).Select(x => x.ProjectWorkTitleB).FirstOrDefaultAsync();
        }
    }
}