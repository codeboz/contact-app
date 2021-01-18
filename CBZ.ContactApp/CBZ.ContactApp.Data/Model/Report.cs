using System;

namespace CBZ.ContactApp.Data.Model
{
    public class Report
    {
        public int Id { get; set; }
        public int ContactCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public string Location { get; set; }
    }
}
