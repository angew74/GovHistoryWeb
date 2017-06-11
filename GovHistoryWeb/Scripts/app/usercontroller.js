app.controller('UserController', function ($scope, $mdDialog, $mdToast, UserService,RoleService) {
    $scope.CreateUser = function () {
        var user =
       {
           UserName: $scope.username,
           Email: $scope.email,
           Firstname: $scope.nome,
           Lastname: $scope.cognome,
           Password: $scope.password,
           ConfirmPassword: $scope.confirmpassword,
           Mobile: $scope.cellulare         
       };
        $("#divLoading").show();
        var response = UserService.CreateUser(user);
        response.then(function (data) {
            if (data.data.success == "true") {
                $scope.msg = "Utente creato";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();               
            } else if (data.data.success == "false") {            
                $scope.msg = "Errore nella ricerca contattare servizio tecnico dettaglio: ";
                $("#divLoading").hide();
                $scope.showConfirm($scope.msg+ data.data.message, $scope.event);
            }
        });
    };
    $scope.selected = [];
    $("#ConfirmDialog").hide();
    $scope.query = {
        order: 'UserName',
        limit: 5,
        page: 1
    };
    

    function successruoli(roles)
    {
        $scope.Ruoli = roles;
    }

    var utenti = UserService.LoadUtenti($scope.query);
    $("#ConfirmDialog").show();
    utenti.then(function (data) {
        if (data.data.success == "true") {
            $("#divLoading").hide();
            if (data.data.ListUtenti != null) {
                this.getUtenti = data.data;
                $scope.getUtenti = data.data;
            }
        } else if (data.data.success == "false") {
            $("#divLoading").hide();
            $scope.msg = "Errore nella ricerca contattare servizio tecnico";
            $scope.showConfirm($scope.msg, $scope.event);
            $scope.getUtenti = data.data;
        }
    });
   


    $scope.getRuoli = function () {
        $scope.promise = RoleService.LoadRuoliByParams($scope.query, successruoli).$promise;
    };

    $scope.editUser = function(id)
    {
        window.location.href = 'UserEdit?id='+id;
    }
    $scope.showRoleDetails = function (roleid)
    {
        window.location.href = '/Admin/RoleDetails?id='+roleid;
    }

    $scope.deleteUserFromRole = function(id, roleid)
    {
        $("#divLoading").show();
        var response = UserService.DeleteUserFromRole(id,roleid);
        response.then(function (data) {
            if (data.data.success == "true") {
                $("#divLoading").hide();
                $scope.msg = "Eliminazione completata";
                $scope.showConfirm($scope.msg, $scope.event);
            }
            else if (data.data.success == "false") {
                $scope.msg = "Errore nella cancellazione contattare servizio tecnico";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            }

        })
    }

    $scope.deleteUser = function(id)
    {
        $("#divLoading").show();
        var response = UserService.DeleteUser(id);
        response.then(function (data) {
            if (data.data.success == "true") {
                $("#divLoading").hide();
                $scope.msg = "Eliminazione completata";
                $scope.showConfirm($scope.msg, $scope.event);
            }
            else if (data.data.success == "false") {
                $scope.msg = "Errore nella cancellazione contattare servizio tecnico";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            }

        })
    }

    $scope.showOTP = function (cont) {
        if (cont == undefined)
        { var testo = "NON PRESENTE"; }
        else { var testo = cont;}
        $mdToast.show(
           $mdToast.simple()
            .textContent("Contatto: " + testo)
           .hideDelay(3000)
           .position('top center')
           )
    };

    $scope.showRoles = function(iduser)
    {
        $scope.query.IdUser = iduser;
        $("#divLoading").show();
        var response = RoleService.LoadRuoliByParams($scope.query);
        response.then(function (data) {
            if (data.data.success == "true") {
                $("#divLoading").hide();
                $("#ListaRuoli").show();               
                if (data.data.ListRuoli != null) {
                    this.getRuoli = data.data;
                    $scope.getRuoli = data.data;
                }
            }
            else if (data.data.success == "false") {
                $scope.msg = "Errore nella visualizzazione dei ruoli utente contattare servizio tecnico";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();               
            }

        })
    }

    $scope.closeToast = function () {
        if (isDlgOpen) return;

        $mdToast
          .hide()
          .then(function () {
              isDlgOpen = false;
          });
    };
    $scope.showConfirm = function (msg, event) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.confirm()
              .title('Creazione non effettuata')
              .textContent(msg)
              .ariaLabel('Errore Creazione')
              .targetEvent(event)
              .ok('Ok')
              .cancel('Cancella');

        $mdDialog.show(confirm).then(function () {
            $scope.status = msg;
        }, function () {
            $scope.status = msg;
        });
    };
});