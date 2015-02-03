'use strict';

app.factory('Courses', ['$resource', function ($resource) {

    console.log('Loading courses-resource.');

    return $resource('api/courses/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);