'use strict';

/* Controllers */

function UserController($scope, $resource, UserData) {
    $scope.SuggestedUser = $resource('/api/suggesteduser/'); /*TODO Look into naming this suggested-user*/
    updateUsers();

    $scope.selectUser = function (user) {
        $scope.selectedUser = user;
    };

    var pusher = new window.Pusher('3aa270fd00dec97e5b04');
    var channel = pusher.subscribe('suggested-user-queue-' + UserData.Username.toString());
    channel.bind('update', function () {
        updateUsers();
    });
    
    function updateUsers() {
        $scope.users = $scope.SuggestedUser.query();
        $(window).resize(); //TODO move this
    }
}