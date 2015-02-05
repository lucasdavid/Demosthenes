'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/courses', {
                templateUrl: '/Templates/courses/index.html',
                controller: 'CoursesController',
                resolve: {
                    resolvedCourses: ['Courses', function (Courses) {
                        return Courses.query().$promise;
                    }],
                    resolvedDepartments: ['Departments', function (Departments) {
                        return Departments.query().$promise;
                    }]
                }
            }).
            when('/courses/:id', {
                templateUrl: '/Templates/courses/course.html',
                controller: 'CoursesDetailsController',
                resolve: {
                    resolvedCourse: ['Courses', '$route', function (Courses, $route) {
                        return Courses.get({ Id: $route.current.params.id });
                    }],
                    resolvedDepartments: ['Departments', function (Departments) {
                        return Departments.query().$promise;
                    }]
                }
            });
    }])