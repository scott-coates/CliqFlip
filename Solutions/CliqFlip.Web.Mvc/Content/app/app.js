'use strict';


// Declare app level module which depends on filters, and services
angular.module('cliqFlipApp', ['ngResource', 'cliqFlipServices', 'cliqFlipDirectives']).
  config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
      $locationProvider.html5Mode(true);
      $routeProvider.when('/u', { templateUrl: '/Content/app/partials/suggested-users.html', controller: UserController });
  } ]);