using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Users;
using System.Collections.Generic;
using System.Linq;
using KrisApp.DataModel.Results;
using System;

namespace KrisApp.DataAccess
{
    public class UserRequestRepo : BaseDAL, IUserRequestRepository
    {
        public UserRequestRepo(string cs) : base(cs)
        {
        }

        public void AddUserRequest(UserRequest userRequest)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.UserRequests.Add(userRequest);
                context.SaveChanges();
            }
        }

        public UserRequest GetUserRequest(int userRequestID)
        {
            UserRequest userRequest = null;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                userRequest = context.UserRequests.AsNoTracking()
                    .Where(x => x.Id == userRequestID).FirstOrDefault();
            }

            return userRequest;
        }

        /// <summary>
        /// Zwraca użytkowników oczekujących na akceptację
        /// </summary>
        public List<UserRequest> GetUserRequests(bool includeGhosts)
        {
            List<UserRequest> userRequests = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                IQueryable<UserRequest> query = context.UserRequests;

                if (includeGhosts == false)
                {
                    query = query.Where(x => x.Ghost == false);
                }

                userRequests = context.UserRequests.AsNoTracking()
                    .Where(x => x.Ghost == false).ToList();
            }

            return userRequests;
        }

        public bool IsUserExisting(string login)
        {
            bool userExists = false;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                //context.Database.Log = LogDb();
                userExists = context.UserRequests.AsNoTracking()
                    .Any(x => x.Login == login);
            }

            return userExists;
        }

        public Result UpdateUserRequestToGhost(int id)
        {
            Result result = new Result();

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                UserRequest req = context.UserRequests.Where(x => x.Id == id).FirstOrDefault();

                if (req != null)
                {
                    req.Ghost = true;
                    context.SaveChanges();

                    result.IsOK = true;
                }
            }

            return result;
        }
    }
}
