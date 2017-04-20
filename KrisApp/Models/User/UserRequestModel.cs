using KrisApp.DataModel.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Models.User
{
    public class UserRequestModel
    {
        public UserRequest UserRequest { get; set; }

        public List<SelectListItem> UserTypes { get; set; }
    }
}