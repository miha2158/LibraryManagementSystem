using System;

namespace LibraryManagementSystem
{
    public class Publication
    {
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }

        public PublicationType PublicationType { get; set; }
    }
}