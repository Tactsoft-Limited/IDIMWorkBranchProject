using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.Exceptions;
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
    public class ProjectDirectorService : BaseService<ProjectDirector>, IProjectDirectorService
    {
        public ProjectDirectorService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<IEnumerable<SelectListItem>> DropdownAsync(int? selected = 0)
        {
            return await _context.ProjectDirectors.Select(d => new SelectListItem
            {
                Text = d.ProjectDirectorName,
                Value = d.ProjectDirectorId.ToString(),
                Selected = d.ProjectDirectorId == selected
            }).ToListAsync();
        }

        public async Task<List<ProjectDirector>> GetAllByProjectId(int id)
        {
            return await _context.ProjectDirectors.Where(x => x.ADPProjectId == id).ToListAsync();
        }

        public async Task<object> GetPagedAsync(ProjectDirectorSearchVm model)
        {
            if (model == null)
                model = new ProjectDirectorSearchVm();

            var query = _context.ProjectDirectors.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.ProjectDirectorName.Contains(model.SearchValue) ||
            x.Designation.Contains(model.SearchValue)) &&
            (!model.JoiningDate.HasValue || x.JoiningDate == model.JoiningDate.Value) &&
            (!model.TransferDate.HasValue || x.TransferDate == model.TransferDate.Value));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ProjectDirectorId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectDirectorVm
                {
                    ProjectDirectorId = x.ProjectDirectorId,
                    ProjectDirectorName = x.ProjectDirectorName,
                    ProjectDirectorNameB = x.ProjectDirectorNameB,
                    Designation = x.Designation,
                    DesignationB = x.DesignationB,
                    JoiningDate = x.JoiningDate,
                    TransferDate = x.TransferDate,
                    PDDocument = x.PDDocument,
                    IsActive = x.IsActive,
                })
            };

            return result;
        }

        public bool IsDuplicateProjectDirector(string name, int? id = null)
        {
            return GetCount(x => x.ProjectDirectorName == name && (id.HasValue || x.ProjectDirectorId != id)) > 0;
        }
    }
}