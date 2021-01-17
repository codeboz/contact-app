using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class ReportStateRepository:IRepository<ReportState>
    {
        private readonly DbSet<ReportState> _dbSet;
        private readonly ContactDbContext _dbContext;


        public ReportStateRepository(ContactDbContext context)
        {
            _dbSet = context.ReportStates;
            _dbContext = context;
        }
        
        public IQueryable<ReportState> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<ReportState> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<ReportState> Where(Func<ReportState, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<ReportState> Add(ReportState t)
        {
            var reportState= await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return reportState.Entity;
        }

        public async Task<ReportState> Update(ReportState t)
        {
            var reportState= _dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return reportState.Entity;        }

        public async Task<ReportState> Remove(ReportState t)
        {
            var reportState= _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return reportState.Entity;
        }
    }
}
