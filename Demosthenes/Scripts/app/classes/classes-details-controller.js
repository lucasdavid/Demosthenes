'use strict';

app.controller('ClassesDetailsController',
    ['$scope', '$location', '$routeParams', 'Validator', 'Calendar',
     'Classes', 'Courses', 'Professors', 'Schedules', 'ClassSchedules', 'resolvedClass',
    function ($scope, $location, $routeParams, Validator, Calendar,
        Classes, Courses, Professors, Schedules, ClassSchedules, resolvedClass) {

        var id = $routeParams.id;
        $scope.loaded     = {};
        $scope.class      = resolvedClass;
        $scope.courses    = Courses.query();
        $scope.professors = Professors.query();
        $scope.schedules  = Schedules.query();
        $scope.daysOfWeek = Calendar.daysOfWeek();
        $scope.terms      = Calendar.terms();
        $scope.years      = Calendar.years();

        $scope.schedules.$promise.then(function () {
            $scope.loaded.schedules = true;
        });

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
            if ($scope.deletedClass !== $scope.class.Course.Title) { return; }

            Classes.delete($scope.class,
                function (data) {
                    toastr.success($scope.class.Course.Title + ' was successfully removed.', 'Success!');
                    $location.path('/classes');
                },
                function (data) {
                    console.log(data);

                    Validator.
                        take(data).
                        toastErrors().
                        otherwiseToastDefaultError();
                });
        }

        $scope.classHas = function (schedule) {

            var schedules = $scope.class.Schedules;

            if ($scope.class.$resolved) {
                for (var i = 0; i < schedules.length; i++) {
                    if (schedules[i].Id == schedule.Id) {
                        return i;
                    }
                }
            }

            return -1;
        }

        $scope.bind = function (schedule) {
            var cs = {
                ClassId: id,
                ScheduleId: schedule.Id
            };

            ClassSchedules.schedule(cs,
                function (data) {
                    $scope.class.Schedules.push(schedule);
                }, function (data) {
                    console.log(data);
                    Validator.
                        take(data).
                        toastWarnings().
                        otherwiseToastDefaultError();
                });
        }

        $scope.unbind = function (schedule) {
            var cs = {
                classId: id,
                scheduleId: schedule.Id
            };

            ClassSchedules.unschedule(cs, function (data) {
                var index = $scope.classHas(schedule);
                if (index > -1) {
                    $scope.class.Schedules.splice(index, 1);
                }
            }, function (data) {
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