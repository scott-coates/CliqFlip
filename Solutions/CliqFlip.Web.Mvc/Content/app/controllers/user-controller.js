'use strict';

/* Controllers */

function UserController($scope, $resource) {
    $scope.SuggestedUser = $resource('/api/suggesteduser/'); /*TODO Look into naming this suggested-user*/
    $scope.users = $scope.SuggestedUser.get(function (data, data2, data3) {
        debugger;
    });
}