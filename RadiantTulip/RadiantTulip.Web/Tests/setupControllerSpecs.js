/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/angular-route.js" />
/// <reference path="../Scripts/app.js" />
/// <reference path="../Scripts/Controllers/setupController.js" />

describe("setup window", function () {
    beforeEach(module('radiant'));
    

    var $controller;

    beforeEach(inject(function (_$controller_, gameTypeService, $q) {
        $controller = _$controller_;

        spyOn(gameTypeService, "gameTypes").and.callFake(function () {
            var deferred = $q.defer();
            deferred.resolve(["AFL", "Wheel Chair Rugby"]);
            return deferred.promise;
        });
    }));

    it("fetches game types from the game type service", inject(function ($rootScope) {
        var $scope = {};

        var controller = $controller("setupController", { $scope: $scope });

        $rootScope.$apply();

        expect($scope.gameTypes).toEqual(["AFL", "Wheel Chair Rugby"]);
    }));
});