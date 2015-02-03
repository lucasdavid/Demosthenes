'use strict';

app.controller('CoursesDetailsController', ['$scope', '$location', '$routeParams', 'Validator', 'resolvedCourse', 'Courses', 'resolvedDepartments',
    function ($scope, $location, $routeParams, Validator, resolvedCourse, Courses, resolvedDepartments) {
        console.log('Loading course-details-controller with id ' + $routeParams.id);

        $scope.course      = resolvedCourse;
        $scope.departments = resolvedDepartments;
        
        $scope.update = function () {
            Courses.update($scope.course,
                function (data) {
                    toastr.info($scope.course.Title + ' was successfully updated.', 'Done!');

                    $scope.clear();
                },
                function (data) {
                    console.log(data);
                    
                    Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastDefaultError();
                });
        }

        $scope.delete = function () {
            // Ignores request, unless user has consciously specified course's name.
            if ($scope.deletedCourse !== $scope.course.Title) { return; }

            Courses.delete($scope.course,
                function (data) {
                    toastr.success($scope.course.Title + ' was successfully removed.', 'Success!');
                    $location.path('/courses');
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
            $scope.deletedCourse = null;
        }
    }]);