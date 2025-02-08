using BGB.Data.StoreProcedure.Pm;
using System.Data.Entity.Core.Objects;

namespace IDIMWorkBranchProject.Data.Database
{
    public partial interface IIDIMDBContextProcedures
    {
        ObjectResult<SpProjectStatusResult> SpProjectStatus();
    }
}
