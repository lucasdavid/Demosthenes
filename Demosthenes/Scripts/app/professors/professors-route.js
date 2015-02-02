'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {
        console.log('Loading professors-route.');

        $routeProvider.
            when('/professors', {
                templateUrl: '/Templates/professors/index.html',
                controller: 'ProfessorsController',
                resolve: {
                    resolvedProfessors: ['Professors', function (Professors) {
                        return Professors.query().$promise;
                    }]
                }
            }).
            when('/professors/:id', {
                templateUrl: '/Templates/professors/department.html',
                controller: 'ProfessorsDetailsController'
            });
    }])