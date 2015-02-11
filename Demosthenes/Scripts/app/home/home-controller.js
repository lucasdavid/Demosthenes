'use strict';

app.controller('HomeController',
    ['$scope', '$location', 'authService',
    function ($scope, $location, authService) {

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/');
        }
        
        $scope.authentication = authService.authentication;
    }]);