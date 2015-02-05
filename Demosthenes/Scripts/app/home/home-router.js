'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/', {
                templateUrl: '/Templates/home/index.html',
                controller: 'HomeController',
            });
    }]);