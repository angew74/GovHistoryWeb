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

    this.DeleteUserFromRole = function (id, roleid)
    {
        var response = $http
      ({
          method: "get",
          url: "/GovHistoryWeb/api/User/DeleteUserFromRole?userid" + id+ "rolesid="+roleid,
          dataType: "json"
      }
      )
    }

    this.LoadRuoli = function (query) {
        var response = $http
   ({
       method: "post",
       url: "/GovHistoryWeb/api/Roles/GetRolesByUser",
       data: JSON.stringify(query),
       dataType: "json"
   });
        return response;
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