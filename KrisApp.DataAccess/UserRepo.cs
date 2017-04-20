using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Users;
using System.Data.Entity;
using System.Linq;
using System;

namespace KrisApp.DataAccess
{
    public class UserRepo : BaseDAL, IUserRepository
    {
        public UserRepo(string cs) : base(cs)
        {
        }

        public void AddUser(User user)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUser(string login, string passwdMd5, bool includeGhosts)
        {
            User user = null;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                //context.Database.Log = LogDb();
                IQueryable<User> query = context.Users.AsNoTracking()
                    .Where(x => x.Login == login && x.Password == passwdMd5)
                    .Include(x => x.Type);

                if (includeGhosts == false)
                {
                    query = query.Where(x => x.Ghost == false);
                }

                user = query.FirstOrDefault();
            }

            return user;
        }
    }
}
