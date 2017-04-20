using KrisApp.DataModel.Users;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IUserRepository
    {

        /// <summary>
        /// Zwraca informację, czy istnieje na bazie użytkownik o danym loginie i haśle
        /// </summary>
        User GetUser(string login, string passwdMd5, bool includeGhosts);
        
        /// <summary>
        /// Dodaje przekazanego Usera na bazę
        /// </summary>
        void AddUser(User newUser);
    }
}
