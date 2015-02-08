'use strict';

app.factory('ClassSchedules', ['$resource', function ($resource) {

    return $resource('api/classes/:classId/schedules/:scheduleId', {}, {
        'query': {
            method: 'GET',
            isArray: true
        },
        'schedule': {
            method: 'POST',
            params: { classId: '@ClassId', scheduleId: '@ScheduleId' }
        },
        'unschedule': {
            method: 'DELETE',
            params: { classId: '@ClassId', scheduleId: '@ScheduleId' }
        }
    });
}]);
