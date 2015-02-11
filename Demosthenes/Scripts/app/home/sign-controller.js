'use strict';

app.controller('SignController', ['$scope', '$location', 'authService', 'Validator',
    function ($scope, $location, authService, Validator) {

        $scope.login = function () {
            authService
                .login($scope.loginData)
                .then(
                    function (response) {
                        $location.path('/');
                    },
                    function (response) {
                        Validator.
                             take(response).
                             toastErrors().
                             otherwiseToastError();
                    }
                );
        }
        
        $scope.clear = function() {
            $scope.loginData = { UserName: '', Password: '' };
        }

        $scope.clear();
    }]);