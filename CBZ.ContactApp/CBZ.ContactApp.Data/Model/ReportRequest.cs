using System;

namespace CBZ.ContactApp.Data.Model
{
    public class ReportRequest
    {
        public  Guid Id { get; set; }
        public  string Location { get; set; }
        public DateTime Requested { get; set; }
        public int ReportStateId { get; set; }
        public virtual ReportState ReportState { get; set; }
        
        public  int ReportId { get; set; }
        
        public virtual Report Report { get; set; }
    }
}
