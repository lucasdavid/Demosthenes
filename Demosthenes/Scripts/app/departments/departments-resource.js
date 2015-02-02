'use strict';

app.factory('Departments', ['$resource', function ($resource) {

    console.log('Loading departments-resource.');

    return $resource('api/departments/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);