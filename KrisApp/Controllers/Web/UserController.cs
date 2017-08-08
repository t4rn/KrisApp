using AutoMapper;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using KrisApp.Models.User;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace KrisApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger _log;
        private readonly IUserService _userSrv;
        private readonly IDictionaryService _dictSrv;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionSrv;

        public UserController(ILogger log, IUserService userSrv, IDictionaryService dictSrv, IMapper mapper, ISessionService sessionSrv)
        {
            _log = log;
            _userSrv = userSrv;
            _dictSrv = dictSrv;
            _mapper = mapper;
            _sessionSrv = sessionSrv;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserResult loginResult = _userSrv.AuthenticateUser(model.Login, model.Password);

            if (loginResult.IsOK)
            {
                _sessionSrv.AddToSession(SessionItem.User, loginResult.User);
                FormsAuthentication.SetAuthCookie(model.Login, false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", loginResult.Message);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            UserRegisterModel model = new UserRegisterModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRequest userRequest = _mapper.Map<UserRegisterModel, UserRequest>(model);

            Result addUserResult = _userSrv.AddUserRequest(userRequest);

            if (addUserResult?.IsOK == true)
            {
                ViewBag.Message = "Konto założone pomyślnie - oczekuj na akceptację administratora.";
                return View(new UserRegisterModel());
            }
            else
            {
                ModelState.AddModelError("", addUserResult.Message);
            }

            return View(model);
        }

        public ActionResult Pending()
        {
            // TODO: dostęp tylko dla admina
            UsersPendingModel model = new UsersPendingModel();
            model.PendingUserRequests = new List<UserRequestModel>();
            List<UserRequest> pendingUsers = _userSrv.GetPendingUsers();
            List<UserType> userTypes = _dictSrv.GetDictionary<UserType>();

            List<SelectListItem> selectList = PrepareUserTypesSelectItemList(userTypes);

            foreach (UserRequest pendingUser in pendingUsers)
            {
                UserRequestModel userreq = new UserRequestModel() { UserRequest = pendingUser, UserTypes = selectList };
                model.PendingUserRequests.Add(userreq);
            }


            return View(model);
        }

        private List<SelectListItem> PrepareUserTypesSelectItemList(List<UserType> userTypes)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (UserType userType in userTypes)
            {
                SelectListItem item = new SelectListItem() { Text = userType.Name, Value = userType.ID.ToString() };
                selectList.Add(item);
            }

            return selectList;
        }

        public ActionResult AcceptRequest(int requestId, int typeId)
        {
            UserResult result = _userSrv.AcceptUserRequest(requestId, typeId);

            return RedirectToAction("Pending");
        }

        public ActionResult RejectRequest(int id)
        {
            Result result = _userSrv.RejectUserRequest(id);

            return RedirectToAction("Pending");
        }

        public ActionResult LogOut()
        {
            _sessionSrv.ClearSession();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }
    }
}