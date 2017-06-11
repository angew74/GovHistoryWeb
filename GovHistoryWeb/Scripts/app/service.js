app.service('LoginService', function ($http) {
    this.UserLogin = function (User) {
        var response = $http({
            method: "post",
            url: "/GovHistoryWeb/Account/Login",
            data: JSON.stringify(User),
            dataType: "json"
        });
        return response;
    }
});

app.service('MenuService', function ($http) {
});

app.service('UserService', function ($http) {
    this.CreateUser = function (user) {
        var response = $http
     ({
         method: "post",
         url: "/GovHistoryWeb/api/User/Create",
         data: JSON.stringify(user),
         dataType: "json"
     });
        return response;
    }
    this.LoadUtenti = function (query) {
        var response = $http
   ({
       method: "post",
       url: "/GovHistoryWeb/api/User/GetAll",
       data: JSON.stringify(query),
       dataType: "json"
   });
        return response;
    }
    this.DeleteUserFromRole = function (id, roleid) {
        var response = $http
      ({
          method: "get",
          url: "/GovHistoryWeb/api/User/DeleteUserFromRole?userid" + id + "rolesid=" + roleid,
          dataType: "json"
      }
      )
    }
    this.DeleteUser = function (id) {
        var response = $http
        ({
            method: "get",
            url: "/GovHistoryWeb/api/User/Delete?userid" + id,
            dataType: "json"
        }
        )
    }

});

app.service('RoleService', function ($http) {
    this.CreateRole = function (role) {
        var response = $http
     ({
         method: "post",
         url: "/GovHistoryWeb/api/Role/Create",
         data: JSON.stringify(role),
         dataType: "json"
     });
        return response;
    }
    this.LoadRuoliByParams = function (query) {
        var response = $http
   ({
       method: "post",
       url: "/GovHistoryWeb/api/Roles/GetRolesByUser",
       data: JSON.stringify(query),
       dataType: "json"
   });
        return response;
    }

});

app.service('PermissionService', function ($http) {

    this.LoadPermessi = function (query) {
        var response = $http
   ({
       method: "post",
       url: "/GovHistoryWeb/api/Permissions/GetRights",
       data: JSON.stringify(query),
       dataType: "json"
   });
        return response;
    }

    this.LoadRuoliByRights = funcion(query)
    {
        var response = $http
 ({
     method: "post",
     url: "/GovHistoryWeb/api/Permissions/GetRoles",
     data: JSON.stringify(right),
     dataType: "json"
 });
        return response;
    }

    this.CreateRight = function (right) {
        var response = $http
     ({
         method: "post",
         url: "/GovHistoryWeb/api/Permissions/Create",
         data: JSON.stringify(right),
         dataType: "json"
     });
        return response;
    }

    this.RemoveRightFromRole(idpermission, idrole)
    {
        var response = $http
({
    method: "get",
    url: "/GovHistoryWeb/api/Permissions/RemoveRole?id=" + idpermission + "&idrole=" + idrole,
    dataType: "json"
});
        return response;
    }

    this.DeleteRight = function (right) {
        var response = $http
     ({
         method: "post",
         url: "/GovHistoryWeb/api/Permissions/Delete",
         data: JSON.stringify(right),
         dataType: "json"
     });
        return response;
    }

});