'use strict';

/* Controllers */

function UserController($scope, $resource, UserData) {
    $scope.SuggestedUser = $resource('/api/suggesteduser/'); /*TODO Look into naming this suggested-user*/
    $scope.users = $scope.SuggestedUser.query();

    var pusher = new window.Pusher('3aa270fd00dec97e5b04');
    var channel = pusher.subscribe('suggested-user-queue-' + UserData.UserId.toString());
    channel.bind('update', function () {});
}