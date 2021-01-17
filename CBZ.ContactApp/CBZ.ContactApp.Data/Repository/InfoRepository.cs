using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class InfoRepository:IRepository<Info>
    {
        private readonly DbSet<Info> _dbSet;
        private readonly ContactDbContext _dbContext;

        public InfoRepository(ContactDbContext context)
        {
            _dbSet = context.Infos;
            _dbContext = context;
        }
        
        public IQueryable<Info> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<Info> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<Info> Where(Func<Info,bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
        
        public async Task<Info> Add(Info t)
        {
            var info = await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return info.Entity;
        }

        public async Task<Info> Update(Info t)
        {
            var info=_dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return info.Entity;
        }

        public async Task<Info> Remove(Info t)
        {
            var info = _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return info.Entity;
        }
    }
}
