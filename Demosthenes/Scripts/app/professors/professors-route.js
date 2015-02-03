'use strict';

app.config(['$routeProvider',
    function ($routeProvider) {
        console.log('Loading professors-route.');

        $routeProvider.
            when('/professors', {
                templateUrl: '/Templates/professors/index.html',
                controller: 'ProfessorsController',
                resolve: {
                    resolvedProfessors: ['Professors', function (Professors) {
                        return Professors.query().$promise;
                    }],
                    resolvedDepartments: ['Departments', function (Departments) {
                        return Departments.query().$promise;
                    }]
                }
            }).
            when('/professors/:id', {
                templateUrl: '/Templates/professors/professor.html',
                controller: 'ProfessorsDetailsController',
                resolve: {
                    resolvedProfessor: ['Professors', '$route', function (Professors, $route) {
                        return Professors.get({ Id: $route.current.params.id });
                    }],
                    resolvedDepartments: ['Departments', function (Departments) {
                        return Departments.query().$promise;
                    }]
                }
            });
    }])