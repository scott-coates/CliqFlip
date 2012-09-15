'use strict';

/* Controllers */

function NavController($scope, $location) {
    $scope.isActive = function (route) {
        return route === $location.path();
    }
}

NavController.$inject = ['$scope', '$location'];