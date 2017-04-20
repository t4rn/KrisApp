using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class UserTypeRepo : BaseDAL, IUserTypeRepository
    {
        public UserTypeRepo(string cs) : base(cs)
        {
        }

        /// <summary>
        /// Zwraca niezduchowane typy użytkowników
        /// </summary>
        public List<UserType> GetUserTypes()
        {
            List<UserType> userTypes = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                userTypes = context.UserTypes.AsNoTracking()
                    .Where(x => x.Ghost == false).ToList();
            }

            return userTypes;
        }
    }
}
