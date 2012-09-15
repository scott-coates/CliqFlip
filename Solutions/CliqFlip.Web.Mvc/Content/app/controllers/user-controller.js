'use strict';

/* Controllers */

function UserController($scope, $resource, UserData) {
    var pusher = new window.Pusher('3aa270fd00dec97e5b04');
    var channel = pusher.subscribe('suggested-user-queue-' + UserData.Username.toString());

    channel.bind('pusher:subscription_succeeded', function (arg) {
        updateUsers();        
    });

    channel.bind('update', function (arg) {
        if (parseInt(arg.usersCount) > 0) {
            updateUsers();
        }
        else {
            alert('no users');
        }
    });

    $scope.SuggestedUser = $resource('/api/suggesteduser/'); /*TODO Look into naming this suggested-user*/

    $scope.selectUser = function (user) {
        $scope.selectedUser = user;
    };

    function updateUsers() {
        $scope.users = $scope.SuggestedUser.query(function (data) {
            console.log("UPDATE USERS : " + data);
        });
        $(window).resize(); //TODO move this
    }
}