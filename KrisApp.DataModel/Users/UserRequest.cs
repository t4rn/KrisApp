using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Users
{
    [Table("AppUserRequest", Schema = "Work")]
    public class UserRequest
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public DateTime AddDate { get; set; }

        public bool Ghost { get; set; }
    }
}