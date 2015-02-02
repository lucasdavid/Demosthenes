'use strict';

app.controller('DepartmentsController', ['$scope', 'resolvedDepartments', 'Departments',
    function ($scope, resolvedDepartments, Departments) {

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
                toastr.error('Something went wrong when trying to save ' + $scope.newDepartment.Name + '.', 'Opps!');
            });
        }

        $scope.clear = function () {
            $scope.newDepartment = { Name: null }
        }

        $scope.clear();
        $scope.departments = resolvedDepartments;
    }]);