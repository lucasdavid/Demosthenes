'use strict';

app.controller('StudentsDetailsController', ['$scope', '$location', '$routeParams', 'Validator',
    'resolvedStudent', 'Students',
    function ($scope, $location, $routeParams, Validator, resolvedStudent, Students) {

        $scope.student = resolvedStudent;

        $scope.update = function () {

            Students.update($scope.student,
                function (data) {
                    toastr.info($scope.student.Name + ' was successfully updated.', 'Done!');

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
            // Ignores request, unless user has consciously specified student's name.
            if ($scope.deletedStudent !== $scope.student.Name) { return; }

            console.log($scope.student);

            Students.delete($scope.student,
                function (data) {
                    toastr.success($scope.student.Name + ' was successfully removed.', 'Success!');
                    $location.path('/students');
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
            $scope.updatedStudent = $scope.deletedStudent = null;
        }

    }]);