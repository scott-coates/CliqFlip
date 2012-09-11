'use strict';


// Declare app level module which depends on filters, and services
angular.module('cliqFlipApp', ['ngResource','cliqFlipServices']).
  config(['$routeProvider', function($routeProvider) {
      $routeProvider.when('/', { templateUrl: '/Content/app/partials/suggested-user-list.html', controller: UserController });
  }]);