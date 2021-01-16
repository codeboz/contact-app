using System;
using System.Collections.Generic;

namespace CBZ.ContactApp.Data.Model
{
    public class Contact
    {
        public  Guid Id { get; set; }
        public  string Name { get; set; }
        public  string Surname { get; set; }
        public  string Company { get; set; }
        public virtual ICollection<Info> Infos { get; set; }
    }
}