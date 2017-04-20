using KrisApp.Common;
using KrisApp.Common.Extensions;
using KrisApp.DataAccess;
using KrisApp.DataModel.Result;
using KrisApp.DataModel.Users;
using KrisApp.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KrisApp.Services
{
    public class UserService : AbstractService
    {
        public UserService(KrisLogger log) : base(log)
        {}

        /// <summary>
        /// Zwraca informację, czy istnieje na bazie niezduchowany użytkownik o danym loginie i haśle
        /// </summary>
        internal UserResult AuthenticateUser(string login, string password)
        {
            UserResult result = new UserResult();

            _log.Debug("[AuthenticateUser] Próba logowania usera '{0}'", login);
            string passwdMd5 = Md5.CreateMd5(password);

            using (KrisDbContext context = new KrisDbContext())
            {
                //context.Database.Log = LogDb();
                User user = context.Users
                    .Where(x => x.Login == login && x.Password == passwdMd5 && x.Ghost == false)
                    .Include(x => x.Type)
                    .FirstOrDefault();

                if (user != null)
                {
                    result.IsOK = true;
                    result.User = user;

                    _log.Debug("[AuthenticateUser] Pomyślnie autoryzowano '{0}'",
                        login);
                }
                else
                {
                    result.Message = $"Brak aktywnego użytkownika o danym loginie i haśle.";
                    _log.Error("[AuthenticateUser] '{0}'", result.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Dodaje prośbę o założenie konta na bazę
        /// </summary>
        internal Result AddUserRequest(UserRegisterModel model)
        {
            Result result = new Result();
            try
            {
                _log.Debug("Próba rejestracji usera = '{0}' o email = '{1}'", model.UserName, model.Email);

                using (KrisDbContext context = new KrisDbContext())
                {
                    //context.Database.Log = LogDb();
                    bool userExists = context.UserRequests.Any(x => x.Login == model.UserName);
                    if (userExists)
                    {
                        result.Message = $"Użytkownik o loginie = '{model.UserName}' już istnieje w systemie!";
                    }
                    else
                    {
                        UserRequest userReq = PrepareUserRequest(model);
                        context.UserRequests.Add(userReq);
                        context.SaveChanges();

                        result.IsOK = true;
                        result.Message = $"Prośba o ID = '{userReq.Id}' utworzona pomyślnie!";
                    }

                    _log.Debug("[AddUserRequest] Wynik dla '{0}' -> '{1}' - {2}",
                        model.UserName, result.IsOK, result.Message);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _log.Error("[AddUserRequest] Ex: {0} {1}", ex.MessageFromInnerEx(), ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Zwraca UserRequest wypełniony danymi z modelu
        /// </summary>
        private UserRequest PrepareUserRequest(UserRegisterModel model)
        {
            UserRequest user = new UserRequest();

            user.Login = model.UserName;
            user.Password = Md5.CreateMd5(model.Password);
            user.Email = model.Email;
            user.Comment = model.Comment;
            user.AddDate = DateTime.Now;

            return user;
        }

        /// <summary>
        /// Tworzy konto użytkownika o przekazanym ID prośby o konto
        /// </summary>
        internal UserResult AcceptUserRequest(int userRequestID, int userTypeID)
        {
            UserResult result = new UserResult();

            try
            {
                using (KrisDbContext context = new KrisDbContext())
                {
                    UserRequest userRequest = context.UserRequests.Where(x => x.Id == userRequestID).FirstOrDefault();

                    if (userRequest != null)
                    {
                        userRequest.Ghost = true;
                        User newUser = PrepareUserFromRequest(userRequest, userTypeID);

                        context.Users.Add(newUser);
                        context.SaveChanges();

                        result.IsOK = true;
                        result.User = newUser;

                        _log.Debug("[AcceptRequest] Pomyślnie dodano usera o requestID = '{0}' -> otrzymał ID = '{1}'",
                            userRequestID, newUser.Id);
                    }
                    else
                    {
                        result.Message = $"Brak requesta o ID = '{userRequestID}'.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _log.Error("[AcceptUserRequest] Ex: {0} {1}", 
                    ex.MessageFromInnerEx(), ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Duchuje request o danym ID
        /// </summary>
        internal Result RejectUserRequest(int id)
        {
            Result result = new Result();

            using (KrisDbContext context = new KrisDbContext())
            {
                UserRequest req = context.UserRequests.Where(x => x.Id == id).FirstOrDefault();

                if (req != null)
                {
                    req.Ghost = true;
                    context.SaveChanges();
                    _log.Debug("[RejectUserRequest] Pomyślnie zduchowano UserRequest o ID = '{0}'", id);
                }
                else
                {
                    _log.Error("[RejectUserRequest] Próba odrzucenia konta o nieistniejącym ID = '{0}'", id);
                }
            }

            return result;
        }

        /// <summary>
        /// Zwraca obiekt klasy User na podstawie danych z przekazanego UserRequest
        /// </summary>
        private User PrepareUserFromRequest(UserRequest userRequest, int userTypeID)
        {
            User u = new User();
            u.AddDate = DateTime.Now;
            u.Email = userRequest.Email;
            u.Login = userRequest.Login;
            u.Password = userRequest.Password;
            u.RequestId = userRequest.Id;
            u.TypeId = userTypeID;
            
            return u;
        }

        /// <summary>
        /// Zwraca użytkowników oczekujących na akceptację
        /// </summary>
        internal List<UserRequest> GetPendingUsers()
        {
            List<UserRequest> userRequests = new List<UserRequest>();

            using (KrisDbContext context = new KrisDbContext())
            {
                userRequests = context.UserRequests.Where(x => x.Ghost == false).ToList();
            }

            return userRequests;
        }
    }
}