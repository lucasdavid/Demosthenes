'use strict';

app.factory('Professors', ['$resource', function ($resource) {

    return $resource('api/professors/:id', {}, {
        'query':   { method: 'GET', isArray: true },
        'get':     { method: 'GET' },
        'update':  { method: 'PUT' }
    });
}]);