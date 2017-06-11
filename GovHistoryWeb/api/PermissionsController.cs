using GovHistoryRepository.Identity;
using GovHistoryRepository.Rbac;
using GovHistoryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GovHistoryWeb.api
{
    public class PermissionsController : ApiController
    {

        [HttpPost]
        [Authorize]
        [Route("api/Permissions/GetRights")]
        public HttpResponseMessage GetPermissions(PermissionsViewModel model)
        {
            try
            {
                List<PERMISSION> rights = new List<PERMISSION>();
                int limit = int.Parse(model.limit);
                int start = 0;
                if (model.page == "1")
                { start = 0; }
                else { start = ((int.Parse(model.page) * int.Parse(model.page)) - 5) + 1; };
                rights = ApplicationRoleManager.GetPermissions(start, limit);
                int count = ApplicationRoleManager.GetPermissionsCount();
                model.ListDiritti = rights;
                model.Totale = count.ToString();
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
        }


        [HttpPost]
        [Authorize]
        [Route("api/Permissions/GetRightsByRole")]
        public HttpResponseMessage GetRightsByRole(PermissionsViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IdRole))
                {
                    model.success = "true";
                    return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
                }
                else
                {
                    int id = int.Parse(model.IdRole);
                    ApplicationRole role = ApplicationRoleManager.GetRole(id);
                    model.success = "true";                    
                    model.ListDiritti = role.PERMISSIONS.ToList();
                    model.Totale = role.PERMISSIONS.Count.ToString();
                }
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Authorize]
        [Route("api/Permissions/RemoveRole")]
        public HttpResponseMessage RemoveRole(string id, string roleid)
        {
            PermissionsViewModel model = new PermissionsViewModel();
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(roleid))
                {
                    model.success = "true";
                    return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
                }
                else
                {
                    int idp = int.Parse(id);
                    int idrole = int.Parse(roleid);
                    if (ApplicationRoleManager.RemovePermission4Role(idrole, idp))
                    {
                        model.success = "true";
                    }
                    else
                    {
                        model.success = "false";
                        model.message = "rimozione non effettuata";
                    }
                  
                }
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);

        }

        [HttpPost]
        [Authorize]
        [Route("api/Permissions/GetRRoles")]
        public HttpResponseMessage GetRoles(PermissionsViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IdPermission))
                {
                    model.success = "true";
                    return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
                }
                else
                {
                    int id = int.Parse(model.IdPermission);
                    List<ApplicationRole> roles = ApplicationRoleManager.GetPermission(id).ROLES;
                    model.success = "true";
                    model.ListGruppi = roles;
                    model.Totale = roles.Count.ToString();
                }
            }

            catch (Exception ex)
            {
                model.success = "false";
                model.message = ex.Message;
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Permissions/Create")]
        public HttpResponseMessage Create(PermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> _errors = new List<string>();
                try
                {
                    PERMISSION newPermission = new PERMISSION()
                    {
                        PermissionDescription = model.description
                    };             
                    if (ApplicationRoleManager.AddPermission(newPermission))
                    {
                        model.success = "true";
                    }
                }
                catch (Exception ex)
                {

                    model.message = ex.Message;
                    model.success = "false";
                }
                if (_errors.Count() > 0)
                {
                    foreach (string e in _errors)
                    {
                        model.message += e;
                    }
                    model.success = "false";
                }
            }
            else
            {
                model.success = "false";
                foreach (var e in ModelState.Values)
                {
                    foreach (var error in e.Errors)
                    {
                        model.message += "Campo non valido " + error.ErrorMessage;
                    }
                }
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Permissions/Delete")]
        public HttpResponseMessage Delete(PermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> _errors = new List<string>();
                try
                {
                    int id = int.Parse(model.IdPermission);
                    if (ApplicationRoleManager.DeletePermission(id))
                    {
                        model.success = "true";
                    }
                }
                catch (Exception ex)
                {

                    model.message = ex.Message;
                    model.success = "false";
                }
                if (_errors.Count() > 0)
                {
                    foreach (string e in _errors)
                    {
                        model.message += e;
                    }
                    model.success = "false";
                }
            }
            else
            {
                model.success = "false";
                foreach (var e in ModelState.Values)
                {
                    foreach (var error in e.Errors)
                    {
                        model.message += "Campo non valido " + error.ErrorMessage;
                    }
                }
            }
            return this.Request.CreateResponse<PermissionsViewModel>(HttpStatusCode.OK, model);
        }
    }
}
