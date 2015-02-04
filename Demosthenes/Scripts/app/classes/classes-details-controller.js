'use strict';

app.controller('ClassesDetailsController', ['$scope', '$location', '$routeParams', 'Validator',
    'resolvedClass', 'Classes', 'resolvedCourses', 'resolvedProfessors',
    function ($scope, $location, $routeParams, Validator, resolvedClass, Classes, resolvedCourses, resolvedProfessors) {
        console.log('Loading class-details-controller with id ' + $routeParams.id);

        $scope.class = resolvedClass;
        $scope.courses = resolvedCourses;
        $scope.professors = resolvedProfessors;
        
        $scope.update = function () {
            Classes.update($scope.class,
                function (data) {
                    toastr.info("#" + $scope.class.Id + ' was successfully updated.', 'Done!');

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
            // Ignores request, unless user has consciously specified class's name.
            if ($scope.deletedClass !== $scope.class.Title) { return; }

            Classes.delete($scope.class,
                function (data) {
                    toastr.success($scope.class.Title + ' was successfully removed.', 'Success!');
                    $location.path('/classes');
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
            $scope.deletedClass = null;
        }
    }]);