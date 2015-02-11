'use strict';

app.controller('ClassesDetailsController', ['$scope', '$location', '$routeParams', 'Validator', 'Calendar',
     'Classes', 'ClassSchedules',
     'resolvedClass', 'resolvedCourses', 'resolvedProfessors', 'resolvedSchedules',
    function ($scope, $location, $routeParams, Validator, Calendar,
        Classes, ClassSchedules,
        resolvedClass, resolvedCourses, resolvedProfessors, resolvedSchedules) {

        var id = $routeParams.id;

        $scope.class = resolvedClass;
        $scope.courses = resolvedCourses;
        $scope.professors = resolvedProfessors;
        $scope.schedules = resolvedSchedules;
        $scope.daysOfWeek = Calendar.daysOfWeek();

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

        $scope.classHas = function (schedule, day) {

            var schedules = $scope.class.ClassSchedules;

            if ($scope.class.$resolved) {
                for (var i = 0; i < schedules.length; i++) {
                    if (schedules[i].ScheduleId == schedule.Id
                        && (schedules[i].DayOfWeek == day || schedules[i].DayOfWeek == $scope.daysOfWeek.indexOf(day))) {
                        return i;
                    }
                }
            }

            return -1;
        }

        $scope.bind = function (schedule, day) {
            var cs = {
                ClassId: id,
                ScheduleId: schedule.Id,
                DayOfWeek: day
            };

            ClassSchedules.schedule(cs, function (data) {
                $scope.class.ClassSchedules.push(cs);
            }, function (data) {
                console.log(data);
                Validator.
                    take(data).
                    toastWarnings().
                    otherwiseToastDefaultError();
            });
        }

        $scope.unbind = function (schedule, day) {
            var cs = {
                classId: id,
                scheduleId: schedule.Id,
                DayOfWeek: day
            };

            ClassSchedules.unschedule(cs, function (data) {
                var index = $scope.classHas(schedule, day);
                if (index > -1) {
                    $scope.class.ClassSchedules.splice(index, 1);
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