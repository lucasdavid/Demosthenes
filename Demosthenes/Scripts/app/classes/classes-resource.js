'use strict';

app.factory('Classes', ['$resource', function ($resource) {

    console.log('Loading classes-resource.');

    return $resource('api/classes/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);
