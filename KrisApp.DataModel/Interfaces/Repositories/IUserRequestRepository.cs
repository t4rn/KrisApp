using System.Collections.Generic;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Results;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IUserRequestRepository
    {
        /// <summary>
        /// Zwraca UserRequest o danym ID
        /// </summary>
        UserRequest GetUserRequest(int userRequestID);

        /// <summary>
        /// Zwraca użytkowników oczekujących na akceptację
        /// </summary>
        List<UserRequest> GetUserRequests(bool includeGhosts);

        /// <summary>
        /// Duchuje request o danym ID
        /// </summary>
        Result UpdateUserRequestToGhost(int id);

        /// <summary>
        /// Zwraca informację, czy istnieje użytkownik o danym loginie
        /// </summary>
        bool IsUserExisting(string login);
        
        /// <summary>
        /// Dodaje przekazany UserRequest na bazę
        /// </summary>
        void AddUserRequest(UserRequest userRequest);
    }
}
