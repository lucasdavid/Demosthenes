'use strict';

app.controller('ProfessorsDetailsController', ['$scope', '$location', '$routeParams', 'Validator',
    'resolvedProfessor', 'Professors', 'resolvedDepartments',
    function ($scope, $location, $routeParams, Validator, resolvedProfessor, Professors, resolvedDepartments) {
        console.log('Loading professor-details-controller with id ' + $routeParams.id);

        $scope.professor = resolvedProfessor;
        $scope.departments = resolvedDepartments;

        $scope.update = function () {
            var professor         = $scope.professor;

            Professors.update(professor,
                function (data) {
                    toastr.info(professor.Name + ' was successfully updated.', 'Done!');

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
            // Ignores request, unless user has consciously specified professor's name.
            if ($scope.deletedProfessor !== $scope.professor.Name) { return; }

            console.log($scope.professor);

            Professors.delete($scope.professor,
                function (data) {
                    toastr.success($scope.professor.Name + ' was successfully removed.', 'Success!');
                    $location.path('/professors');
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
            $scope.updatedProfessor = $scope.deletedProfessor = null;
        }

    }]);