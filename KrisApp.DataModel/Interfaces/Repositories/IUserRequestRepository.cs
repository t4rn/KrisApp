using System.Collections.Generic;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Results;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IUserRequestRepository
    {
        /// <summary>
        /// Returns UserRequest of the given ID
        /// </summary>
        UserRequest GetUserRequest(int userRequestID);

        /// <summary>
        /// Returns user requests waiting for acceptance
        /// </summary>
        List<UserRequest> GetUserRequests(bool includeGhosts);

        /// <summary>
        /// Ghosts user request of the given ID
        /// </summary>
        Result UpdateUserRequestToGhost(int id);

        /// <summary>
        /// Checks, if exists a user with the given login
        /// </summary>
        bool IsUserExisting(string login);
        
        /// <summary>
        /// Adds UserRequest do database
        /// </summary>
        void AddUserRequest(UserRequest userRequest);
    }
}
