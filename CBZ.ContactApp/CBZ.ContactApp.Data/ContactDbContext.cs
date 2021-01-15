using System;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }
    }
}