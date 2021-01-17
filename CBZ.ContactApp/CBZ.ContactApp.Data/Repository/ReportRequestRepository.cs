using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class ReportRequestRepository:IRepository<ReportRequest>
    {
        private readonly DbSet<ReportRequest> _dbSet;
        private readonly ContactDbContext _dbContext;


        public ReportRequestRepository(ContactDbContext context)
        {
            _dbSet = context.ReportRequests;
            _dbContext = context;
        }
        
        public IQueryable<ReportRequest> Get()
        {
            return _dbSet.AsQueryable();
        }

        public  async Task<ReportRequest> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<ReportRequest> Where(Func<ReportRequest, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<ReportRequest> Add(ReportRequest t)
        {
            var reportRequest= await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return reportRequest.Entity;
        }

        public async Task<ReportRequest> Update(ReportRequest t)
        {
            var reportRequest= _dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return reportRequest.Entity;
        }

        public async Task<ReportRequest> Remove(ReportRequest t)
        {
            var reportRequest= _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return reportRequest.Entity;
        }
    }
}
