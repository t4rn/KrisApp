using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Contact
{
    [Table("ContactMessage", Schema = "WWW")]
    public class ContactMessage
    {
        public int ID { get; set; }

        public string Author { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string IP { get; set; }

        public DateTime AddDate { get; set; }
    }
}