'use strict';

/* Controllers */

function UserController($scope, $resource, UserData) {
    var pusher = new window.Pusher(UserData.PusherAppKey);
    var channel = pusher.subscribe('suggested-user-queue-' + UserData.Username.toString());
    $scope.users = [];

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
        setTimeout("$(window).resize();"); //TODO move this
    };

    function updateUsers() {
        if (typeof UserData.suggestedUsers !== "undefined") {
            $scope.users = UserData.suggestedUsers;
            $scope.$apply();
        }
        else {
            $scope.SuggestedUser.query(function (data) {
                $scope.users = UserData.suggestedUsers = data;
            });
        }
        $(window).resize(); //TODO move this
    }
}

UserController.$inject = ['$scope', '$resource', 'UserData'];