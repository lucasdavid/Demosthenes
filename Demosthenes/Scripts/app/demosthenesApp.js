'use strict';

var app = angular.module("demosthenesApp", ['ngRoute', 'ngResource']);

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