'use strict';

app.controller('ProfessorsDetailsController', ['$scope', '$location', '$routeParams', 'resolvedProfessor', 'Professors',
    function ($scope, $location, $routeParams, resolvedProfessor, Professors) {
        console.log('Loading professor-details-controller with id ' + $routeParams.id);

        $scope.professor = resolvedProfessor;
        setTimeout(function () {
            console.log($scope.professor.toJSON());
        }, 1000);
        
        $scope.update = function () {
            var professor         = $scope.professor;
            $scope.professor.Name = $scope.updatedProfessor;

            Professors.update(professor,
                function (data) {
                    $scope.professor = Professors.get({ Id: $routeParams.id });
                    toastr.info(professor.Name + ' was successfully updated.', 'Done!');

                    $scope.clear();
                },
                function (data) {
                    console.log(data);
                    toastr.error('Something went wrong.', 'Opps!');
                });
        }

        $scope.delete = function () {
            // Ignores request, unless user has consciously specified professor's name.
            if ($scope.deletedProfessor !== $scope.professor.Name) { return; }

            Professors.delete($scope.professor,
                function (data) {
                    toastr.success($scope.professor.Name + ' was successfully removed.', 'Success!');
                    $location.path('/professors');
                },
                function (data) {
                    console.log(data);
                    toastr.error('Something went wrong.', 'Opps!');
                });
        }

        $scope.clear = function () {
            $scope.updatedProfessor = $scope.deletedProfessor = null;
        }

    }]);