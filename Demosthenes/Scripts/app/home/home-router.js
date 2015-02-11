'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/', {
                templateUrl: '/Templates/home/index.html',
                controller: 'HomeController',
            }).
            when('/sign', {
                templateUrl: '/Templates/home/sign.html',
                controller: 'SignController'
            });
    }]);