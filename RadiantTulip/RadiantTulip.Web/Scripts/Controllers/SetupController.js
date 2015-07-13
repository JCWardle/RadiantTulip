angular.module('radiant')
.controller("setupController", ["$scope", "gameTypeService", "groundService",
    function setupController($scope, gameTypeService, groundService) {
        $scope.positionalData = {};
        gameTypeService.gameTypes().then(function(d) { 
            $scope.gameTypes = d
        });

        groundService.grounds().then(function (d) {
            $scope.grounds = d;
        });

        $scope.changeGameType = function () {
            angular.forEach($scope.grounds, function (value) {
                if (value.Type.replace(/ /g, "") === $scope.gameType) {
                    $scope.ground = value;
                    return;
                }
            });
        };

        $scope.changeGround = function () {
            angular.forEach($scope.gameTypes, function (value) {
                if (value.replace(/ /g, "") === $scope.ground.Type) {
                    $scope.gameType = value;
                    return;
                }
            });
        };
}]);