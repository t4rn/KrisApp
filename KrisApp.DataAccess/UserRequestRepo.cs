using KrisApp.DataModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrisApp.DataAccess
{
    public class UserRequestRepo : BaseDAL, IUserRequestRepository
    {
        public UserRequestRepo(string cs) : base(cs)
        {
        }
    }
}
