using GovHistoryRepository.Identity;
using GovHistoryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GovHistoryWeb.api
{
    public class RolesController : ApiController
    {


        [HttpPost]
        [Authorize]
        [Route("api/Roles/GetRoles")]
        public HttpResponseMessage GetRoles(ApplicationRolesViewModel model)
        {
            try
            {
                List<ApplicationRole> roles = new List<ApplicationRole>();
                int limit = int.Parse(model.limit);
                int start = 0;
                if (model.page == "1")
                { start = 0; }
                else { start = ((int.Parse(model.page) * int.Parse(model.page)) - 5) + 1; };
                roles = ApplicationRoleManager.GetRoles(start, limit);
                int count = ApplicationRoleManager.GetRolesCount();      
                model.ListRuoli = roles;
                model.Totale = count.ToString();
            }
            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<ApplicationRolesViewModel>(HttpStatusCode.OK, model);
        }


        [HttpPost]
        [Authorize]
        [Route("api/Roles/GetRolesByUser")]
        public HttpResponseMessage GetRolesByUser(ApplicationRolesViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IdUser))
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
                    foreach (var role in user.Roles)
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

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Role/Create")]
        public HttpResponseMessage Create(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                List<string> _errors = new List<string>();
                try
                {
                    ApplicationRole newRole = new ApplicationRole()
                    {
                        IsSysAdmin = role.IsSysAdmin,
                        LastModified = System.DateTime.Now,
                        Name = role.Name,
                        RoleDescription = role.RoleDescription
                    };
                    if (ApplicationRoleManager.CreateRole(newRole))
                    {
                        role.success = "true";
                    }
                }
                catch (Exception ex)
                {

                    role.message = ex.Message;
                    role.success = "false";
                }
                if (_errors.Count() > 0)
                {
                    foreach (string e in _errors)
                    {
                        role.message += e;
                    }
                    role.success = "false";
                }
            }
            else
            {
                role.success = "false";
                foreach (var e in ModelState.Values)
                {
                    foreach (var error in e.Errors)
                    {
                        role.message += "Campo non valido " + error.ErrorMessage;
                    }
                }
            }
            return this.Request.CreateResponse<RoleViewModel>(HttpStatusCode.OK, role);
        }

    }
}