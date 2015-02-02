'use strict';

app.controller('DepartmentsDetailsController', ['$scope', '$location', '$routeParams', 'Departments',
    function ($scope, $location, $routeParams, Departments) {
        console.log('Loading department-details-controller with id ' + $routeParams.id);

        $scope.department = Departments.get({ Id: $routeParams.id });
        
        $scope.update = function () {
            var department         = $scope.department;
            $scope.department.Name = $scope.updatedDepartment;

            Departments.update(department,
                function (data) {
                    $scope.department = Departments.get({ Id: $routeParams.id });
                    toastr.info(department.Name + ' was successfully updated.', 'Done!');

                    $scope.clear();
                },
                function (data) {
                    console.log(data);
                    toastr.error('Something went wrong.', 'Opps!');
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
                    toastr.error('Something went wrong.', 'Opps!');
                });
        }

        $scope.clear = function () {
            $scope.updatedDepartment = $scope.deletedDepartment = null;
        }

    }]);