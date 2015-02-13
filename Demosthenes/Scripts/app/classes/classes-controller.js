'use strict';

app.controller('ClassesController',
    ['$scope', 'Validator',
    'resolvedClasses', 'resolvedCourses', 'resolvedProfessors',
    'Classes', 'Calendar',
    
    function ($scope, Validator, resolvedClasses, resolvedCourses, resolvedProfessors, Classes, Calendar) {

        $scope.classes    = resolvedClasses;
        $scope.courses    = resolvedCourses;
        $scope.professors = resolvedProfessors;
        $scope.terms      = Calendar.terms();
        $scope.years      = Calendar.years();

        function getItemById(array, id) {
            for (var i = 0; i < array.length; i++) {
                if (array[i]["Id"] === id) {
                    return array[i];
                }
            }

            return null;
        }
        function getCourseById(id) {
            return getItemById($scope.courses, id);
        }
        function getProfessorById(id) {
            return getItemById($scope.professors, id);
        }

        $scope.create = function () {
            Classes.save($scope.newClass,
            function (_class) {
                toastr.success('Class #' + _class.Id + ' saved!', 'Success!');
                $scope.clear();

                _class.Course    = getCourseById(_class.CourseId);
                _class.Professor = getProfessorById(_class.ProfessorId);
                $scope.classes.push(_class);
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