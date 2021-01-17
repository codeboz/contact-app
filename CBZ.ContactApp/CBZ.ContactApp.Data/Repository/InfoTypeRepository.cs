using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class InfoTypeRepository:IRepository<InfoType>
    {
        private readonly DbSet<InfoType> _dbSet;
        private readonly ContactDbContext _dbContext;

        public InfoTypeRepository(ContactDbContext context)
        {
            _dbSet = context.InfoTypes;
            _dbContext = context;
        }
        
        public IQueryable<InfoType> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<InfoType> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<InfoType> Where(Func<InfoType, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<InfoType> Add(InfoType t)
        {
            var infoType=await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return infoType.Entity;
        }

        public async Task<InfoType> Update(InfoType t)
        {
            var infoType=_dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return infoType.Entity;
        }

        public async Task<InfoType> Remove(InfoType t)
        {
            var infoType= _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return infoType.Entity;
        }
    }
}
