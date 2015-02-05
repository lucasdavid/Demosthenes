'use strict';

console.log('Demosthenes app has started.');

var app = angular.module("demosthenesApp", ['ngRoute', 'ngResource']);

// Validator
// Takes <param>data<param>, a object that represents the outcome of a HTTP requisition an tries to interpret its ModelState property.
app.factory('Validator', [function () {
    return {
        _data: null,
        valid: function () {
            return this._data && this._data.data && this._data.data.ModelState;
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
                var modelState = this._data.data.ModelState;
                for (var err in modelState) {
                    // Ignore $id attribute.
                    if (err === '$id') continue;
                    // Merge current set of errors to result.
                    result = result.concat(modelState[err]);
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
            if (this.valid()) {
                for (var i = 0; i < this.errors().length; i++) {
                    toastr.error(this.errors()[i], 'Error!');
                }
            }

            return this;
        },
        otherwiseToastDefaultError: function () {
            if (!this.valid()) {
                toastr.error('Sorry, something went terribly wrong!', 'Error!');
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
            return daysOfWeek.slice(); // deep copy daysOfWeek
        },
        timeFrames: function () {
            return [
                '08:00',
                'B', 'C'];
        },
        timeFromString: function (data) {
            return data
                ? data.getHours() + ":" + data.getMinutes() + ":" + data.getSeconds()
                : null;
        }
    };
}])