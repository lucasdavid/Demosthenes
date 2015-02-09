'use strict';

app.controller('EnrollmentController', ['$http', '$scope', '$routeParams',
    'Validator',
    'Departments', 'Classes',
    function ($http, $scope, $routeParams, Validator, Departments, Classes) {

        $scope.departmentId = $routeParams.departmentId;
        $scope.courseId = $routeParams.courseId;

        if ($scope.departmentId == 'start') {
            $scope.done = true;
        } else if (!$scope.departmentId) {
            $scope.departments = Departments.query();
            $scope.departments.$promise.then(function () {
                $scope.done = true
            });
        } else if (!$scope.courseId) {
            getCoursesOf($scope.departmentId);
        } else {
            getClassesOf($scope.courseId);
        }

        function getCoursesOf(id) {
            $scope.done = false;

            $http.
                get('/api/courses/departments/' + id).
                success(function (data, status) {
                    $scope.courses = data;
                    $scope.done = true;
                }).
                error(function (data, status) {
                    Validator.
                        take(data).
                        toastErrors().
                        otherwiseToastDefaultError();

                    $scope.done = true;
                });
        }

        function getClassesOf(id) {
            $scope.done = false;

            $http.
                get('/api/classes/courses/' + id).
                success(function (data, status) {
                    $scope.classes = data;
                    $scope.done = true;
                }).
                error(function (data, status) {
                    console.log(data);
                    Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastDefaultError();

                    $scope.done = true;
                });
        }

        $scope.clear = function () {
        }

        $scope.clear();
    }]);