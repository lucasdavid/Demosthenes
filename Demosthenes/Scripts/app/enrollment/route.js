'use strict';

app.config(['$routeProvider',
    function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/enrollment/:departmentId?/:courseId?', {
                templateUrl: '/Templates/enrollment/index.html',
                controller: 'EnrollmentController'
            });
    }
]);