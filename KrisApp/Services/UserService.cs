using KrisApp.Common;
using KrisApp.Common.Extensions;
using KrisApp.DataAccess;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using KrisApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.Services
{
    public class UserService : AbstractService
    {
        private readonly IUserRequestRepository _userRequestRepo;
        private readonly IUserRepository _userRepo;

        public UserService(KrisLogger log) : base(log)
        {
            _userRequestRepo = new UserRequestRepo(Properties.Settings.Default.csDB);
            _userRepo = new UserRepo(Properties.Settings.Default.csDB);
        }

        /// <summary>
        /// Zwraca informację, czy istnieje na bazie niezduchowany użytkownik o danym loginie i haśle
        /// </summary>
        internal UserResult AuthenticateUser(string login, string password)
        {
            UserResult result = new UserResult();

            _log.Debug("[AuthenticateUser] Próba logowania usera '{0}'", login);
            string passwdMd5 = Md5.CreateMd5(password);

            result.User = _userRepo.GetUser(login, passwdMd5, false);

            if (result.User != null)
            {
                result.IsOK = true;

                _log.Debug("[AuthenticateUser] Pomyślnie autoryzowano '{0}'",
                    login);
            }
            else
            {
                result.Message = $"Brak aktywnego użytkownika o danym loginie i haśle.";
                _log.Error("[AuthenticateUser] '{0}'", result.Message);
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

                bool userExists = _userRequestRepo.IsUserExisting(model.UserName);

                if (userExists)
                {
                    result.Message = $"Użytkownik o loginie = '{model.UserName}' już istnieje w systemie!";
                }
                else
                {
                    UserRequest userReq = PrepareUserRequest(model);
                    _userRequestRepo.AddUserRequest(userReq);

                    result.IsOK = true;
                    result.Message = $"Prośba o ID = '{userReq.Id}' utworzona pomyślnie!";
                }

                _log.Debug("[AddUserRequest] Wynik dla '{0}' -> '{1}' - {2}",
                    model.UserName, result.IsOK, result.Message);

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
                UserRequest userRequest = _userRequestRepo.GetUserRequest(userRequestID);

                if (userRequest != null)
                {
                    userRequest.Ghost = true;
                    User newUser = PrepareUserFromRequest(userRequest, userTypeID);

                    _userRepo.AddUser(newUser);

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
            Result result = _userRequestRepo.UpdateUserRequestToGhost(id);

            if (result.IsOK)
            {
                _log.Debug("[RejectUserRequest] Pomyślnie zduchowano UserRequest o ID = '{0}'", id);
            }
            else
            {
                _log.Error("[RejectUserRequest] Próba odrzucenia konta o nieistniejącym ID = '{0}'", id);
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
            List<UserRequest> userRequests = _userRequestRepo.GetUserRequests(false);

            return userRequests;
        }
    }
}