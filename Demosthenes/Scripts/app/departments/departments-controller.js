'use strict';

app.controller('DepartmentsController', ['$scope', 'Validator', 'resolvedDepartments', 'Departments',
    function ($scope, Validator, resolvedDepartments, Departments) {

        $scope.departments = resolvedDepartments;

        $scope.create = function () {
            Departments.save($scope.newDepartment,
            function (department) {
                toastr.success('Department "' + $scope.newDepartment.Name + '" saved!', 'Success!');
                $scope.clear();

                $scope.departments.push(department);
            },
            function (data) {
                console.log(data);
                Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastError();
            });
        }

        $scope.clear = function () {
            $scope.newDepartment = { Name: null }
        }

        $scope.clear();
    }]);