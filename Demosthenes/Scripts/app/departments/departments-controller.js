'use strict';

app.controller('DepartmentsController', ['$scope', 'Validator', 'resolvedDepartments', 'Departments',
    function ($scope, Validator, resolvedDepartments, Departments) {

        console.log('Loading department-controller.');

        $scope.create = function () {
            Departments.save($scope.newDepartment,
            function (data) {
                toastr.success('Department "' + $scope.newDepartment.Name + '" saved!', 'Success!');
                $scope.clear();

                $scope.departments = Departments.query();
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
            $scope.newDepartment = { Name: null }
        }

        $scope.clear();
        $scope.departments = resolvedDepartments;
    }]);