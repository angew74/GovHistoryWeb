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
