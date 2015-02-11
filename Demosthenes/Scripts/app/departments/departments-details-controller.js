'use strict';

app.controller('DepartmentsDetailsController',
    ['$http', '$scope', '$location', '$routeParams', 'Validator', 'resolvedDepartment', 'Departments',
    function ($http, $scope, $location, $routeParams, Validator, resolvedDepartment, Departments) {

        $scope.department = resolvedDepartment;
        $scope.loaded = {};

        $http
            .get('/api/courses/departments/' + $routeParams.id)
            .success(function (data, status) {
                $scope.department.Courses = data;
                $scope.loaded.courses = true;
            })
            .error(function (data) {
                Validator.
                    take(data).
                    toastWarnings().
                    otherwiseToastError('Loading of courses associated with this department has failed.', 'Opps!');
            });

        $http
            .get('/api/professors/departments/' + $routeParams.id)
            .success(function (data, status) {
                $scope.department.Professors = data;
                $scope.loaded.professors = true;

            })
            .error(function (data) {
                console.log(data);
                $scope.loaded.professors = true;

                Validator.
                    take(data).
                    toastWarnings().
                    otherwiseToastError('Loading of professors associated with this department has failed.', 'Opps!');
            });

        $scope.update = function () {
            Departments.update($scope.department,
                function (data) {
                    toastr.info($scope.department.Name + ' was successfully updated.', 'Done!');

                    $scope.clear();
                },
                function (data) {
                    console.log(data);

                    Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastError();
                });
        }

        $scope.delete = function () {
            // Ignores request, unless user has consciously specified department's name.
            if ($scope.deletedDepartment !== $scope.department.Name) { return; }

            Departments.delete($scope.department,
                function (data) {
                    toastr.success($scope.department.Name + ' was successfully removed.', 'Success!');
                    $location.path('/departments');
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
            $scope.deletedDepartment = null;
        }

    }]);