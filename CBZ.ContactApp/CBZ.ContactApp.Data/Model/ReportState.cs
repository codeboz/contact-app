using System.Collections.Generic;

namespace CBZ.ContactApp.Data.Model
{
    public class ReportState
    {
        public  int Id { get; set; }
        public  string Name { get; set; }
        public virtual ICollection<ReportRequest> ReportRequest { get; set; } 
    }
}