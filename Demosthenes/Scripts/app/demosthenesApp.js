'use strict';

console.log('Demosthenes app has started.');

var app = angular.module("demosthenesApp", ['ngRoute', 'ngResource', 'LocalStorageModule']);

app.factory('authService',
    ['$http', '$q', 'localStorageService',
    function ($http, $q, localStorageService) {

        var serviceBase = '/';
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            UserName: ""
        };

        var _saveRegistration = function (registration) {
            _logOut();

            return $http.
                post(serviceBase + 'api/account/register', registration).
                then(function (response) {
                    return response;
                });
        };

        var _login = function (loginData) {
            var data = "grant_type=password&UserName=" + loginData.UserName + "&Password=" + loginData.Password;

            var deferred = $q.defer();

            $http.
                post(serviceBase + 'token', data, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).
                success(function (response) {
                    localStorageService.set('authorizationData',
                    { token: response.access_token, UserName: loginData.UserName });

                    _authentication.isAuth = true;
                    _authentication.UserName = loginData.UserName;

                    deferred.resolve(response);

                }).
                error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var _logOut = function () {
            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.UserName = "";
        };

        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.UserName = authData.UserName;
            }
        }

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;

        return authServiceFactory;
    }]);

app.factory('authInterceptorService',
    ['$q', '$location', 'localStorageService',
    function ($q, $location, localStorageService) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                $location.path('/sign');
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

// Validator
// Takes <param>data<param>, a object that represents the outcome of a HTTP requisition an tries to interpret its ModelState property.
app.factory('Validator', [function () {
    return {
        _data: null,
        valid: function () {
            return this._data
                && (this._data.data && (this._data.data.ModelState || this._data.data.Message)
                    || this._data.error_description);
        },
        take: function (data) {
            if (data) {
                this._data = data;
            }

            return this;
        },
        errors: function () {
            var result = [];

            if (this.valid()) {
                // ModelStates, for field validation
                if (this._data.data) {
                    var modelState = this._data.data.ModelState;
                    for (var err in modelState) {
                        // Ignore $id attribute.
                        if (err === '$id') continue;
                        // Merge current set of errors to result.
                        result = result.concat(modelState[err]);
                    }

                    var message = this._data.data.Message;
                    if (message) {
                        result.push(message);
                    }
                }

                // Errors descriptors, for API communication
                if (this._data.error_description) {
                    result.push(this._data.error_description);
                }
            }

            return result;
        },
        toastWarnings: function () {
            var errors = this.errors();
            for (var i = 0; i < errors.length; i++) {
                toastr.warning(errors[i], 'Warning!');
            }

            return this;
        },
        toastErrors: function () {
            var errors = this.errors();
            for (var i = 0; i < errors.length; i++) {
                toastr.error(errors[i], 'Error!');
            }

            return this;
        },
        // Deprecated
        otherwiseToastDefaultError: function () {
            if (!this.valid()) {
                toastr.error('Sorry, something went terribly wrong!', 'Error!');
            }
        },
        otherwiseToastError: function (message, title) {
            if (!this.valid()) {
                message = message || 'Sorry, something went terribly wrong!';
                title = title || 'Error!';
                toastr.error(message, title);
            }
        }
    };
}]);

// Calendar
// Util for classes' schedules.
app.factory('Calendar', [function () {
    var daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

    return {
        daysOfWeek: function () {
            // copies daysOfWeek
            return daysOfWeek.slice();
        },
        // Used to create the week calendar.
        cartesianSchedulesDaysOfWeek: function (schedules, classId) {
            var result = {};

            for (var currDay = 0; currDay < daysOfWeek.length; currDay++) {
                var day = daysOfWeek[currDay];

                result[day] = [];

                for (var currSchedule = 0; currSchedule < schedules.length; currSchedule++) {
                    var id = schedules[currSchedule].Id;
                    result[day].push({
                        DayOfWeek: day,
                        ScheduleId: id,
                        ClassId: classId
                    });
                }
            }

            return result;
        },
        timeFromString: function (data) {
            return data
                ? data.getHours() + ":" + data.getMinutes() + ":" + data.getSeconds()
                : null;
        }
    };
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
