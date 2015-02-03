'use strict';

app.controller('StudentsController', ['$scope', 'resolvedStudents', 'Students', 'Validator',
    function ($scope, resolvedStudents, Students, Validator) {

        console.log('Loading student-controller.');

        $scope.create = function () {
            Students.save($scope.newStudent,
            function (student) {
                toastr.success('Student "' + $scope.newStudent.Name + '" saved!', 'Success!');
                $scope.clear();

                $scope.students.push(student);
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
            $scope.newStudent = { Email: null, Name: null, Password: null, ConfirmPassword: null }
        }

        $scope.clear();
        $scope.students = resolvedStudents;
    }]);