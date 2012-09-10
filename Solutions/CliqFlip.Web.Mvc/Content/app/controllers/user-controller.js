'use strict';

/* Controllers */

function UserController($scope, $resource) {
    $scope.SuggestedUser = $resource('/api/suggested-user/');
    $scope.users = $scope.SuggestedUser.get();
}