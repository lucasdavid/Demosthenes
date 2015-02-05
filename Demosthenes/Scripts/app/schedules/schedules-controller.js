'use strict';

app.controller('SchedulesController', ['$scope', 'Validator', 'Calendar', 'resolvedSchedules', 'Schedules',
    function ($scope, Validator, Calendar, resolvedSchedules, Schedules) {
        $scope.schedules = resolvedSchedules;
        $scope.daysOfWeek = Calendar.daysOfWeek();
        $scope.timeFromString = Calendar.timeFromString;

        $scope.create = function () {
            var newSchedule = {
                DayOfWeek: $scope.newSchedule.DayOfWeek,
                TimeStarted: Calendar.timeFromString($scope.newSchedule.TimeStarted),
                TimeFinished: Calendar.timeFromString($scope.newSchedule.TimeFinished)
            }

            Schedules.save(newSchedule,
            function (schedule) {
                toastr.success('Schedule #' + newSchedule.Id + ' saved!', 'Success!');
                $scope.clear();

                $scope.schedules.push(schedule);
                console.log(schedule);
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
            $scope.newSchedule = { DayOfWeek: "", TimeStarted: null, TimeFinished: null };
        }

        $scope.clear();
    }]);