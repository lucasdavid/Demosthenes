'use strict';

app.controller('ClassesController', ['$scope', 'Validator', 'resolvedClasses', 'Classes', 'resolvedCourses', 'resolvedProfessors',
    function ($scope, Validator, resolvedClasses, Classes, resolvedCourses, resolvedProfessors) {
        console.log('Loading class-controller.');

        $scope.classes    = resolvedClasses;
        $scope.courses    = resolvedCourses;
        $scope.professors = resolvedProfessors;

        $scope.create = function () {
            Classes.save($scope.newClass,
            function (_class) {
                toastr.success('Class #' + _class.Id + ' saved!', 'Success!');
                $scope.clear();

                $scope.classes.push(_class);
                console.log(_class);
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
            $scope.newClass = { ProfessorId: "", CourseId: "", Size: 60, Enrollable: true }
        }

        $scope.clear();
    }]);