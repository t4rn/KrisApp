using KrisApp.DataModel.Results;
using KrisApp.Models.User;
using KrisApp.Services;
using System.Web.Mvc;
using System.Web.Security;

namespace KrisApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly KrisLogger _log;
        private readonly UserService _userSrv;
        private readonly DictionaryService _dictSrv;

        public UserController()
        {
            _log = new KrisLogger();
            _userSrv = new UserService(_log);
            _dictSrv = new DictionaryService(_log);
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
                SessionService.AddToSession(SessionService.SessionItem.User, loginResult.User);
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

            Result addUserResult = _userSrv.AddUserRequest(model);

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
            UsersPendingModel model = _userSrv.PrepareUsersPendingModel();

            return View(model);
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
            SessionService.ClearSession();
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