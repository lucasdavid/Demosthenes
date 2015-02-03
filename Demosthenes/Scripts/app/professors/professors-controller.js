'use strict';

app.controller('ProfessorsController', ['$scope', 'resolvedProfessors', 'Professors', 'resolvedDepartments', 'Validator',
    function ($scope, resolvedProfessors, Professors, resolvedDepartments, Validator) {

        console.log('Loading professor-controller.');

        $scope.create = function () {
            Professors.save($scope.newProfessor,
            function (professor) {
                toastr.success('Professor "' + $scope.newProfessor.Name + '" saved!', 'Success!');
                $scope.clear();

                $scope.professors.push(professor);
                $scope.displayCreateForm = false;
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
            $scope.newProfessor = { Email: null, Name: null, Password: null, ConfirmPassword: null }
        }

        $scope.clear();
        $scope.professors = resolvedProfessors;
        $scope.departments = resolvedDepartments;
    }]);