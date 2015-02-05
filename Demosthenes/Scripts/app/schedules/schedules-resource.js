'use strict';

app.factory('Schedules', ['$resource', function ($resource) {

    return $resource('api/schedules/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);