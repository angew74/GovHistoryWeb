app.controller('RoleController', function ($scope, $mdDialog, $mdToast, RoleService) {
    $("#ConfirmDialog").hide();
    $scope.CreateRole = function () {
        var role =
       {
           IsSysAdmin : $scope.issysadmin,          
           Name : $scope.name,
           RoleDescription : $scope.roledescription       
       };
        $("#divLoading").show();
        var response = RoleService.CreateRole(role);
        response.then(function (data) {
            if (data.data.success == "true") {
                $scope.msg = "Ruolo creato";
                $scope.showConfirm($scope.msg, $scope.event);
                $("#divLoading").hide();
            } else if (data.data.success == "false") {
                $scope.msg = "Errore nella creazione contattare servizio tecnico dettaglio: ";
                $("#divLoading").hide();
                $scope.showConfirm($scope.msg + data.data.message, $scope.event);
            }
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