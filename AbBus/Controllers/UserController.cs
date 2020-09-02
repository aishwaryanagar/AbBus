using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using AbBus.BAL;
using AbBus.Authentication;
using AbBus.BAL.ViewModels;

namespace AbBus.Controllers
{
    [System.Web.Mvc.Route("api/[controller]")]
    public class UserController : ApiController
    {

        private readonly UserManagement _usrManagement;
        UserController()
        {
            _usrManagement = new UserManagement();
        }

        [System.Web.Mvc.Route("sign-in")]
        [System.Web.Http.HttpGet()]
        public ActionResult SignIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email not entered");
            if (string.IsNullOrEmpty(password))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Password not entered");
            try
            {
                var user = _usrManagement.SignIn(email, password);
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Data not found");
                return new HttpStatusCodeResult(HttpStatusCode.OK, new AuthenticationModule().GenerateTokenForUser(user.Name,user.Id,user.Email));
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [JWTAuthenticationFilter]
        [System.Web.Http.HttpGet()]
        [System.Web.Mvc.Route("edit-profile")]
        public ActionResult EditProfile(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Name not entered");
            if (string.IsNullOrEmpty(email))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email not entered");
            try
            {
                if (!_usrManagement.EditProfile(name, email))
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Data not found");
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Profile details changed");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to save changes. Try again, and if the problem persists, see your system administrator." + ex.Message);
            }
        }

        [System.Web.Mvc.Route("sign-up")]
        [System.Web.Http.HttpPost()]
        public ActionResult SignUp(UserVM userVMObj)
        {
            if (string.IsNullOrEmpty(userVMObj.Email))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email not entered");
            if (string.IsNullOrEmpty(userVMObj.Name))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Name not entered");
            if (string.IsNullOrEmpty(userVMObj.Password))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Password not entered");
            try
            {
                var isUserSignedUp = _usrManagement.SignUp(userVMObj);
                if (!isUserSignedUp)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Account already exists");
                return new HttpStatusCodeResult(HttpStatusCode.OK,"You have signed up successfully");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to save changes. Try again, and if the problem persists, see your system administrator." + ex.Message);
            }
        }
    }
}