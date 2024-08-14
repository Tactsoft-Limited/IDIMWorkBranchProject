using BGB.Data.StoreProcedure.Pm;
using System.Data.Entity.Core.Objects;

namespace BGB.Data.Database
{
    public partial interface IIDIMDBContextProcedures
    {
        ObjectResult<SpProjectStatusResult> SpProjectStatus();
    }
}
