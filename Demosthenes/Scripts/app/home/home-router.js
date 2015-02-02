'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {
        console.log('Loading home-route');

        $routeProvider.
            when('/', {
                templateUrl: '/Templates/home/index.html',
                controller: 'HomeController',
            });
    }]);