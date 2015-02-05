'use strict';

app.factory('Students', ['$resource', function ($resource) {

    return $resource('api/students/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);