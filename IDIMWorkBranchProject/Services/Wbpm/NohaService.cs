using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class NohaService : BaseService<Noha>, INohaService
    {
        public NohaService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<Noha> GetByProjectWorkIdAsync(int id)
        {
            return await _context.Nohas.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}