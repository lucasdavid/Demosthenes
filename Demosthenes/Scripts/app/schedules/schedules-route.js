'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {

        $routeProvider.
            when('/schedules', {
                templateUrl: '/Templates/schedules/index.html',
                controller: 'SchedulesController',
                resolve: {
                    resolvedSchedules: ['Schedules', function (Schedules) {
                        return Schedules.query().$promise;
                    }]
                }
            });
    }]);
