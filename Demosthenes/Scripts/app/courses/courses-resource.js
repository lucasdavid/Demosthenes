'use strict';

app.factory('Courses', ['$resource', function ($resource) {

    return $resource('api/courses/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);