angular.module('cliqFlipServices', ['ngResource']).
    factory('UserData', function () {
        return window['CliqFlip.App.UserData'];
    });