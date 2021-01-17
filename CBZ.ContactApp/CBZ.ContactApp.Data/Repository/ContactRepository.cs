using System;
using System.Linq;
using System.Threading.Tasks;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data.Repository
{
    public class ContactRepository:IRepository<Contact>
    {
        private readonly DbSet<Contact> _dbSet;
        private readonly ContactDbContext _dbContext;

        public ContactRepository(ContactDbContext context)
        {
            _dbSet = context.Contacts;
            _dbContext = context;
        }
        
        public IQueryable<Contact> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<Contact> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<Contact> Where(Func<Contact, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<Contact> Add(Contact t)
        {
            var contact = await _dbSet.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return contact.Entity;
        }

        public async Task<Contact> Update(Contact t)
        {
            var contact =_dbSet.Update(t);
            await _dbContext.SaveChangesAsync();
            return contact.Entity;
        }

        public async Task<Contact> Remove(Contact t)
        {
            var contact = _dbSet.Remove(t);
            await _dbContext.SaveChangesAsync();
            return contact.Entity;
        }
    }
}
