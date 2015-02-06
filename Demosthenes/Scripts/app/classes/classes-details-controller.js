'use strict';

app.controller('ClassesDetailsController', ['$scope', '$location', '$routeParams', 'Validator', 'Calendar',
    'resolvedClass', 'Classes', 'resolvedCourses', 'resolvedProfessors', 'resolvedSchedules',
    function ($scope, $location, $routeParams, Validator, Calendar, resolvedClass, Classes,
        resolvedCourses, resolvedProfessors, resolvedSchedules) {

        var id = $routeParams.id;

        $scope.class = resolvedClass;
        $scope.courses = resolvedCourses;
        $scope.professors = resolvedProfessors;

        $scope.schedules = resolvedSchedules;
        $scope.daysOfWeek = Calendar.daysOfWeek();

        // Save original schedule arrays so it can be restored, in case the user ask for it.
        resolvedClass.$promise.then(function () {
            $scope.originalClassSchedules = $scope.class.Schedules.slice();
        });

        // Create all possible combinations of Schedules X DayOfWeek.
        resolvedSchedules.$promise.then(function () {
            $scope.cartesian = Calendar.cartesianSchedulesDaysOfWeek(resolvedSchedules, id);
        });

        $scope.update = function () {
            Classes.update($scope.class,
                function (data) {
                    toastr.info("#" + $scope.class.Id + ' was successfully updated.', 'Done!');

                    $scope.originalClassSchedules = $scope.class.Schedules.slice();
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

        $scope.classHas = function (schedule) {
            var schedules = $scope.class.Schedules;

            for (var i = 0; i < schedules.length; i++) {
                var assertSameClassId = schedules[i].ClassId == schedule.ClassId;
                var assertSameScheduleId = schedules[i].ScheduleId == schedule.ScheduleId;
                var assertSameDayOfWeek = schedules[i].DayOfWeek == schedule.DayOfWeek || schedules[i].DayOfWeek == $scope.daysOfWeek.indexOf(schedule.DayOfWeek);

                if (assertSameClassId && assertSameScheduleId && assertSameDayOfWeek) {
                    return i;
                }
            }

            return -1;
        }

        $scope.bind = function (schedule) {
            var hasSchedule = $scope.classHas(schedule) > -1;
            if (!hasSchedule) {
                $scope.scheduleChanged = true;
                $scope.class.Schedules.push(schedule);
                console.log($scope.class.Schedules);
            }
        }

        $scope.unbind = function (schedule) {
            var index = $scope.classHas(schedule);
            if (index > -1) {
                $scope.scheduleChanged = true;
                $scope.class.Schedules.splice(index, 1);
                console.log($scope.class.Schedules);
            }
        }

        $scope.clear = function () {
            $scope.deletedClass = null;
            $scope.class.Schedules = $scope.originalClassSchedules.slice();
        }
    }]);