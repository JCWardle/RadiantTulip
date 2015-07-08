angular.module('radiant')
.controller("setupController", function setupController($scope) {
    $scope.positionalData = {};
    $scope.gameTypes = [ "AFL", "Wheel Chair Rugby" ];
    $scope.fields = [{ name: "Pattersons", type: "AFL" }, { name: "Standard Court", type: "Wheel Chair Rugby" }];
});