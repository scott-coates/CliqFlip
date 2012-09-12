'use strict';


// Declare app level module which depends on filters, and services
angular.module('cliqFlipApp', ['ngResource', 'cliqFlipServices', 'cliqFlipDirectives']).
  config(['$routeProvider', function ($routeProvider) {
      $routeProvider.when('/', { templateUrl: '/Content/app/partials/suggested-users.html', controller: UserController });
  } ]);