using GovHistoryRepository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Net;
using GovHistoryWeb.Models;

namespace GovHistoryWeb.api
{
    public class UserController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/User/GetAll")]
        public HttpResponseMessage GetAll(ApplicationUsersViewModel model)
        {
            try
            {
                int limit = int.Parse(model.limit);
                int start = 0;
                if (model.page == "1")
                { start = 0; }
                else { start = ((int.Parse(model.page) * int.Parse(model.page)) - 5) + 1; };
                var utenti = ApplicationUserManager.GetUsers(start, limit);
                model.ListUtenti = utenti;
                model.Totale = utenti.Count.ToString();
               model.success = "true";

            }
            catch (Exception ex)
            {
                List<string> m = new List<string>();
                m.Add(ex.Message);
                model.message = m;
                model.success = "false";
            }
            return this.Request.CreateResponse<ApplicationUsersViewModel>(HttpStatusCode.OK, model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/User/Create")]
        public HttpResponseMessage Create(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                List<string> _errors = new List<string>();
                try
                {
                    RBACStatus _retVal = RBAC_ExtendedMethods.Register(user, this.UserManager, this.SignInManager, out _errors);
                    switch (_retVal)
                    {
                        case RBACStatus.Success:
                            {
                                user.message = "Account correttamente creato.  E' ora possibile autenticarsi...";
                                user.returnUrl = "Confirmation";
                                user.success = "true";
                            }
                            break;
                        case RBACStatus.RequiresAccountActivation:
                            {
                                user.returnUrl = "ConfirmEmailSent";
                                user.success = "true";
                            }
                            break;
                        case RBACStatus.EmailVerification:
                            {
                                user.success = "true";
                                user.returnUrl = "RequestEmailVerification";

                            }
                            break;
                        case RBACStatus.PhoneVerification:
                            {
                                user.success = "true";
                                user.returnUrl = "OTP4PhoneVerification";
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {

                    user.message = ex.Message;
                    user.success = "false";
                }
                if (_errors.Count() > 0)
                {
                    foreach (string e in _errors)
                    {
                        user.message += e;
                    }
                    user.success = "false";
                }
            }
            else
            {
                user.success = "false";
                foreach (var e in ModelState.Values)
                {
                    foreach (var error in e.Errors)
                    {
                        user.message += "Campo non valido " + error.ErrorMessage;
                    }
                }
            }
            return this.Request.CreateResponse<RegisterViewModel>(HttpStatusCode.OK, user);
        }

        [HttpGet]
        [Authorize]
        [Route("api/User/Delete")]
        public HttpResponseMessage Delete(string id)
        {
            ApplicationUsersViewModel model = new ApplicationUsersViewModel();
            try
            {
                int userId = int.Parse(id);
                ApplicationUserManager.DeleteUser(userId);
                model.success = "true";
            }
            catch (Exception ex)
            {

                model.message.Add(ex.Message);
                model.success = "false";
            }
            return this.Request.CreateResponse<ApplicationUsersViewModel>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Authorize]
        [Route("api/User/DeleteUserFromRole")]
        public HttpResponseMessage DeleteUserFromRole(string userid, string role)
        {
            ApplicationUsersViewModel model = new ApplicationUsersViewModel();
            try
            {
                int id = int.Parse(userid);
                int roleid = int.Parse(role);
                model.success = "true";
                ApplicationUserManager.RemoveUser4Role(id, roleid);
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message.Add(ex.Message);
            }
            return this.Request.CreateResponse<ApplicationUsersViewModel>(HttpStatusCode.OK, model);
        }

    }
}