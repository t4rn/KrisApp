using KrisApp.DataModel.Dictionaries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Users
{
    [Table("AppUser", Schema = "Work")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        public UserType Type { get; set; }

        [ForeignKey("UserCreateRequest")]
        public int RequestId { get; set; }

        public UserRequest UserCreateRequest { get; set; }

        public DateTime AddDate { get; set; }

        public bool Ghost { get; set; }
    }
}