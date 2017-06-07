using KrisApp.Common;
using KrisApp.Common.Extensions;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using KrisApp.Models.User;
using System;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IUserRequestRepository _userRequestRepo;
        private readonly IUserRepository _userRepo;
        private readonly IDictionaryService _dictSrv;

        public UserService(ILogger log, IUserRepository userRepo, IUserRequestRepository userRequestRepo, IDictionaryService dictSrv) : base(log)
        {
            _userRequestRepo = userRequestRepo;
            _userRepo = userRepo;
            _dictSrv = dictSrv;
        }

        /// <summary>
        /// Zwraca informację, czy istnieje na bazie niezduchowany użytkownik o danym loginie i haśle
        /// </summary>
        public UserResult AuthenticateUser(string login, string password)
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
        public Result AddUserRequest(UserRequest userReq)
        {
            Result result = new Result();
            try
            {
                _log.Debug("Próba rejestracji usera = '{0}' o email = '{1}'", userReq.Login, userReq.Email);

                bool userExists = _userRequestRepo.IsUserExisting(userReq.Login);

                if (userExists)
                {
                    result.Message = $"Użytkownik o loginie = '{userReq.Login}' już istnieje w systemie!";
                }
                else
                {
                    FillUserRequest(userReq);
                    _userRequestRepo.AddUserRequest(userReq);

                    result.IsOK = true;
                    result.Message = $"Prośba o ID = '{userReq.Id}' utworzona pomyślnie!";
                }

                _log.Debug("[AddUserRequest] Wynik dla '{0}' -> '{1}' - {2}",
                    userReq.Login, result.IsOK, result.Message);

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
        private void FillUserRequest(UserRequest user)
        {
            user.Password = Md5.CreateMd5(user.Password);
            user.AddDate = DateTime.Now;
        }

        /// <summary>
        /// Tworzy konto użytkownika o przekazanym ID prośby o konto
        /// </summary>
        public UserResult AcceptUserRequest(int userRequestID, int userTypeID)
        {
            UserResult result = new UserResult();

            UserRequest userRequest = _userRequestRepo.GetUserRequest(userRequestID);

            if (userRequest != null)
            {
                userRequest.Ghost = true;
                User newUser = PrepareUserFromRequest(userRequest, userTypeID);

                _userRepo.AddUser(newUser);

                result.IsOK = true;
                result.User = newUser;

                // TODO: update userRequest

                _log.Debug("[AcceptRequest] Pomyślnie dodano usera o requestID = '{0}' -> otrzymał ID = '{1}'",
                    userRequestID, newUser.Id);
            }
            else
            {
                result.Message = $"Brak requesta o ID = '{userRequestID}'.";
            }

            return result;
        }

        /// <summary>
        /// Duchuje request o danym ID
        /// </summary>
        public Result RejectUserRequest(int id)
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
        /// Zwraca oczekujących na akceptację użytkowników
        /// </summary>
        public List<UserRequest> GetPendingUsers()
        {
            UsersPendingModel model = new UsersPendingModel();
            model.PendingUserRequests = new List<UserRequestModel>();
            List<UserRequest> pendingUsers = _userRequestRepo.GetUserRequests(false);

            return pendingUsers;
        }
    }
}