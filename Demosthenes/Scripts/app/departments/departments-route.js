'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/departments', {
                templateUrl: '/Templates/departments/index.html',
                controller: 'DepartmentsController',
                resolve: {
                    resolvedDepartments: ['Departments', function (Departments) {
                        return Departments.query().$promise;
                    }]
                }
            }).
            when('/departments/:id', {
                templateUrl: '/Templates/departments/department.html',
                controller: 'DepartmentsDetailsController',
                resolve: {
                    resolvedDepartment: ['Departments', '$route', function (Departments, $route) {
                        return Departments.get({ Id: $route.current.params.id });
                    }]
                }
            });
    }])