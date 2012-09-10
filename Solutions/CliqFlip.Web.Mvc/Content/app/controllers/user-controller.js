'use strict';

/* Controllers */

function UserController($scope, $resource) {
    $scope.SuggestedUser = $resource('/api/suggesteduser/');
    $scope.users = $scope.SuggestedUser.get();
}