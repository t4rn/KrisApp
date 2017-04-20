using KrisApp.DataModel.Dictionaries;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IUserTypeRepository
    {
        List<UserType> GetUserTypes();
    }
}
