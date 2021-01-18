using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class ReportRepository:IRepository<Report>
    {
        private readonly DbSet<Report> _dbSet;
        private readonly ContactDbContext _dbContext;


        public ReportRepository(ContactDbContext context)
        {
            _dbSet = context.Reports;
            _dbContext = context;
        }
        
        public IQueryable<Report> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<Report> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<Report> Where(Func<Report, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<Report> Add(Report t)
        {
            var Report= await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return Report.Entity;
        }

        public async Task<Report> Update(Report t)
        {
            var Report= _dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return Report.Entity;        }

        public async Task<Report> Remove(Report t)
        {
            var Report= _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return Report.Entity;
        }
    }
}
