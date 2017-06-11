app.controller('PermissionsController', function ($scope, $mdDialog, $mdToast, PermissionService) {
    $("#ConfirmDialog").hide();
    $scope.query = {
        order: 'UserName',
        limit: 5,
        page: 1
    };
    var rights = PermissionService.LoadPermessi($scope.query);
    $("#ConfirmDialog").show();
    rights.then(function (data) {
        if (data.data.success == "true") {
            $("#divLoading").hide();
            if (data.data.ListDiritti != null) {
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

    $scope.CreatePermission = function () {
        var perm =
       {
           PermissionDescription: $scope.permissiondescription         
       };
        $("#divLoading").show();
        var response = PermissionService.CreateRight(perm);
        response.then(function (data) {
            if (data.data.success == "true") {
                $scope.msg = "Permesso creato";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            } else if (data.data.success == "false") {
                $scope.msg = "Errore nella creazione contattare servizio tecnico dettaglio: ";
                $("#divLoading").hide();
                $scope.showConfirm($scope.msg + data.data.message, $scope.event);
            }
        });
    };
      
    $scope.DeletePermission = function (id)
    {
        var perm =
      {
          PermissionId: id
      };
        $("#divLoading").show();
        var response = PermissionService.DeleteRight(perm);
        response.then(function (data) {
            if (data.data.success == "true") {
                $scope.msg = "Diritto Cancellato";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            } else if (data.data.success == "false") {
                $scope.msg = "Errore nella cancellazione contattare servizio tecnico dettaglio: ";
                $("#divLoading").hide();
                $scope.showConfirm($scope.msg + data.data.message, $scope.event);
            }
        });
    }

    $scope.showRoleDetails = function (roleid) {
        window.location.href = '/Admin/RoleDetails?id=' + roleid;
    }

    $scope.showRoles = function (id) {
        $scope.query.IdPermission = id;
        $("#divLoading").show();
        var response = PermissionService.LoadRuoliByRights($scope.query);
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

    $scope.deleteRoleFromPermission = function(idpermission,idrole)
    {
        $("#divLoading").show();
        var response = PermissionService.RemoveRightFromRole(idpermission,idrole);
        response.then(function (data) {
            if (data.data.success == "true") {
                $scope.msg = "Diritto Rimosso dal Ruolo";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            } else if (data.data.success == "false") {
                $scope.msg = "Errore nella rimozione contattare servizio tecnico dettaglio: ";
                $("#divLoading").hide();
                $scope.showConfirm($scope.msg + data.data.message, $scope.event);
            }
        });
    }

    $scope.addPermissionToRole = function(idpermission)
    {

    }

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