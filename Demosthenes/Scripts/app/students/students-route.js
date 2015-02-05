'use strict';

app.config(['$routeProvider',
    function ($routeProvider) {

        $routeProvider.
            when('/students', {
                templateUrl: '/Templates/students/index.html',
                controller: 'StudentsController',
                resolve: {
                    resolvedStudents: ['Students', function (Students) {
                        return Students.query().$promise;
                    }]
                }
            }).
            when('/students/:id', {
                templateUrl: '/Templates/students/student.html',
                controller: 'StudentsDetailsController',
                resolve: {
                    resolvedStudent: ['Students', '$route', function (Students, $route) {
                        return Students.get({ Id: $route.current.params.id });
                    }]
                }
            });
    }])