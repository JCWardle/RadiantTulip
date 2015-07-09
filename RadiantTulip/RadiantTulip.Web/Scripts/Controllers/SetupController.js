angular.module('radiant')
.controller("setupController", ["$scope", "gameTypeService", function setupController($scope, gameTypeService) {
    $scope.positionalData = {};
    gameTypeService.gameTypes().then(function(d) { 
        $scope.gameTypes = d
    });
    $scope.fields = [{ name: "Pattersons", type: "AFL" }, { name: "Standard Court", type: "Wheel Chair Rugby" }];
}]);