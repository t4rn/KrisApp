using System.Collections.Generic;
using KrisApp.DataModel.Users;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IUserRequestRepository
    {
        List<UserRequest> GetUserRequests(bool includeGhosts);
    }
}
