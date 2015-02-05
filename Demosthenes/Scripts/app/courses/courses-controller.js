'use strict';

app.controller('CoursesController', ['$scope', 'Validator', 'resolvedCourses', 'Courses', 'resolvedDepartments',
    function ($scope, Validator, resolvedCourses, Courses, resolvedDepartments) {

        $scope.create = function () {
            Courses.save($scope.newCourse,
            function (course) {
                toastr.success('Course "' + $scope.newCourse.Title + '" saved!', 'Success!');
                $scope.clear();

                $scope.courses.push(course);
                console.log(course);
            },
            function (data) {
                console.log(data);

                Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastDefaultError();
            });
        }

        $scope.clear = function () {
            $scope.newCourse = { Title: null, Credits: null, DepartmentId: "" }
        }

        $scope.clear();
        $scope.courses     = resolvedCourses;
        $scope.departments = resolvedDepartments;
    }]);