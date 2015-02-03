'use strict';

app.controller('DepartmentsDetailsController', ['$scope', '$location', '$routeParams', 'Validator', 'resolvedDepartment', 'Departments',
    function ($scope, $location, $routeParams, Validator, resolvedDepartment, Departments) {
        console.log('Loading department-details-controller with id ' + $routeParams.id);

        $scope.department = resolvedDepartment;
        
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
                        otherwiseToastDefaultError();
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