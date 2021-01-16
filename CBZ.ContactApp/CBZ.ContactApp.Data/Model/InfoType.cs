using System.Collections.Generic;

namespace CBZ.ContactApp.Data.Model
{
    public class InfoType
    {
        public  int Id { get; set; }
        public  string Name { get; set; }
        public ICollection<Info> Infos { get; set; }
    }
}