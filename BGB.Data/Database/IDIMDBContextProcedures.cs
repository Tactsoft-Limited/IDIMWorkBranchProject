using BGB.Data.StoreProcedure.Pm;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace BGB.Data.Database
{
    public partial class IDIMDBEntities
    {
        private IIDIMDBContextProcedures _procedures;

        protected void OnModelCreatingGeneratedProcedures(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpProjectStatusResult>().ToTable(null);
        }

        public IIDIMDBContextProcedures GetProcedures() { return Procedures; }

        public virtual IIDIMDBContextProcedures Procedures
        {
            get
            {
                if (_procedures is null)
                    _procedures = new IDIMDBContextProcedures(this);
                return _procedures;
            }
            set => _procedures = value;
        }
    }

    public partial class IDIMDBContextProcedures : IIDIMDBContextProcedures
    {
        private readonly IDIMDBEntities _context;

        public IDIMDBContextProcedures(IDIMDBEntities context) { _context = context; }

        public ObjectResult<SpProjectStatusResult> SpProjectStatus()
        {
            return ((IObjectContextAdapter)_context).ObjectContext.ExecuteFunction<SpProjectStatusResult>("sp_ProjectStatus");
        }
    }
}
