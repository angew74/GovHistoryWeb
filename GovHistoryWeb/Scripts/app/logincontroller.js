app.controller('LoginController', function ($scope, $mdDialog, LoginService) {
    $scope.status = '  ';
    $scope.customFullscreen = false;
    $("#ConfirmDialog").hide();
    $scope.LoginCheck = function ($event) {
        var User = {
            UserName: $scope.uName,
            Password: $scope.password,
            RememberMe: $scope.rememberme
        };
        $("#divLoading").show();
        $scope.event = $event;
        var getData = LoginService.UserLogin(User);
        $scope.msg = "";
        getData.then(function (msg) {
            if (msg.data.success == "false") {
                $("#divLoading").hide();
                //  $("#ConfirmDialog").modal('show');
                if (msg.data.ResponseUrl != null)
                {
                    window.location.href = 'GovHistoryWeb/Account/' + msg.data.ResponseUrl;
                }
                if (msg.data.message == null) {
                    $scope.msg = "Errore nell'autenticazione contattare servizio tecnico";
                }
                else {
                    for(var i=0;i<msg.data.message.length;i++)
                    {
                        $scope.msg += msg.data.message[i];
                    }
                }
                $scope.showConfirm($scope.msg, $scope.event);
            }
            else if (msg.data.success == "true" && msg.data.Error != null) {
                $("#divLoading").hide();
                // $("#ConfirmDialog").modal('show');
                $scope.msg = msg.data.Error;
                $scope.showConfirm($scope.msg, $scope.event);
            }
            else if (msg.data.success == "true" && (msg.data.ResponseUrl != "" && msg.data.ResponseUrl != null)) {
                uID = msg.data;
                $("#divLoading").hide();
                window.location.href = "/" + msg.data.ResponseUrl;
            }
        });
    }

    $scope.showConfirm = function (msg, event) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.confirm()
              .title('Attenzione autenticazione non effettuata')
              .textContent(msg)
              .ariaLabel('Lucky day')
              .targetEvent(event)
              .ok('Ok')
              .cancel('Cancella');

        $mdDialog.show(confirm).then(function () {
            $scope.status = msg;
        }, function () {
            $scope.status = msg;
        });
    };


    function clearFields() {
        $scope.uName = '';
        $scope.password = '';
        $scope.department = '';
    }



});



