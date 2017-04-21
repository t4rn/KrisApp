using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Zwraca informację, czy istnieje na bazie niezduchowany użytkownik o danym loginie i haśle
        /// </summary>
        UserResult AuthenticateUser(string login, string password);

        /// <summary>
        /// Dodaje prośbę o założenie konta na bazę
        /// </summary>
        Result AddUserRequest(UserRequest userReq);

        /// <summary>
        /// Tworzy konto użytkownika o przekazanym ID prośby o konto
        /// </summary>
        UserResult AcceptUserRequest(int userRequestID, int userTypeID);

        /// <summary>
        /// Duchuje request o danym ID
        /// </summary>
        Result RejectUserRequest(int id);

        /// <summary>
        /// Zwraca użytkowników oczekujących na akceptację
        /// </summary>
        List<UserRequest> GetPendingUsers();
    }
}
