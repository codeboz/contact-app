using System;

namespace CBZ.ContactApp.Data.Model
{
    public class Info
    {
        public string Data { get; set; }
        public int InfoTypeId { get; set; }
        public virtual InfoType InfoType { get; set; }
        public Guid ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}