using GovHistoryRepository.Identity;
using GovHistoryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GovHistoryWeb.api
{
    public class RolesController : ApiController
    {
        [HttpPost]
        [Authorize]
        [Route("api/Roles/GetRolesByUser")]
        public HttpResponseMessage GetRolesByUser(ApplicationRolesViewModel model)
        {           
            try
            {
                if(string.IsNullOrEmpty(model.IdUser))
                {
                    model.success = "true";
                    return this.Request.CreateResponse<ApplicationRolesViewModel>(HttpStatusCode.OK, model);
                }
                else
                {
                    int id = int.Parse(model.IdUser);
                    ApplicationUser user = ApplicationUserManager.GetUser(id);
                    model.success = "true";
                    List<ApplicationRole> roles = new List<ApplicationRole>();
                    foreach(var role in user.Roles)
                    {
                        roles.Add(role.Role);
                    }
                    model.ListRuoli = roles;
                    model.Totale = roles.Count.ToString();
                }
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<ApplicationRolesViewModel>(HttpStatusCode.OK, model);
        }

    }
}