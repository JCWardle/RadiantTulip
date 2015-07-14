angular.module('radiant')
.controller("setupController", ["$scope", "gameTypeService", "groundService", "gameService",
    function setupController($scope, gameTypeService, groundService, gameService) {
        $scope.positionalData = {};
        gameTypeService.gameTypes().then(function(d) { 
            $scope.gameTypes = d
        });

        $scope.fileInput = "derp";

        groundService.grounds().then(function (d) {
            $scope.grounds = d;
        });

        $scope.changeGameType = function () {
            angular.forEach($scope.grounds, function (value) {
                if (value.Type === $scope.gameType.replace(/ /g, "")) {
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

        $scope.toggleAdvancedSettings = function () {
            $scope.showAdvancedSettings = !$scope.showAdvancedSettings;
        }

        $scope.showAdvancedSettings = false;

        $scope.createGame = function () {
            gameService.createGame($scope.ground, $scope.fileInput);
        }
}]);