'use strict';

app.controller('ProfessorsController', ['$scope', 'resolvedProfessors', 'Professors',
    function ($scope, resolvedProfessors, Professors) {

        console.log('Loading professor-controller.');

        $scope.create = function () {
            Professors.save($scope.newProfessor,
            function (data) {
                toastr.success('Professor "' + $scope.newProfessor.Name + '" saved!', 'Success!');
                $scope.clear();

                $scope.professors = Professors.query();
            },
            function (data) {
                console.log(data);
                toastr.error('Something went wrong when trying to save ' + $scope.newProfessor.Name + '.', 'Opps!');
            });
        }

        $scope.clear = function () {
            $scope.newProfessor = { Name: null }
        }

        $scope.clear();
        $scope.professors = resolvedProfessors;

    }]);