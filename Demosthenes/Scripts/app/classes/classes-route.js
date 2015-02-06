'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/classes', {
                templateUrl: '/Templates/classes/index.html',
                controller: 'ClassesController',
                resolve: {
                    resolvedClasses: ['Classes', function (Classes) {
                        return Classes.query().$promise;
                    }],
                    resolvedProfessors: ['Professors', function (Professors) {
                        return Professors.query().$promise;
                    }],
                    resolvedCourses: ['Courses', function (Courses) {
                        return Courses.query().$promise;
                    }]
                }
            }).
            when('/classes/:id', {
                templateUrl: '/Templates/classes/class.html',
                controller: 'ClassesDetailsController',
                resolve: {
                    resolvedClass: ['Classes', '$route', function (Classes, $route) {
                        return Classes.get({ Id: $route.current.params.id });
                    }],
                    resolvedProfessors: ['Professors', function (Professors) {
                        return Professors.query().$promise;
                    }],
                    resolvedCourses: ['Courses', function (Courses) {
                        return Courses.query().$promise;
                    }],
                    resolvedSchedules: ['Schedules', function (Schedules) {
                        return Schedules.query().$promise;
                    }]
                }
            });
    }])