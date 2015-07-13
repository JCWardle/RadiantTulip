/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/angular-route.js" />
/// <reference path="../Scripts/app.js" />
/// <reference path="../Scripts/Controllers/setupController.js" />
/// <reference path="../Scripts/Services/gameTypeService.js" />
/// <reference path="../Scripts/Services/groundService.js" />

describe("setup window", function () {
    beforeEach(module('radiant'));
    var $controller;
    var grounds =
        [{
            CentreLongitude: 100,
            CentreLatitude: 200,
            Height: 50,
            Width: 50,
            Padding: 200,
            Type: "AFL",
            Name: "Game A",
            Rotation: 5
        },
        {
            CentreLongitude: 300,
            CentreLatitude: 400,
            Height: 100,
            Width: 50,
            Padding: 100,
            Type: "Wheel Chair Rugby",
            Name: "Game B",
            Rotation: 1
        }];

    beforeEach(inject(function (_$controller_, gameTypeService, groundService, $q) {
        $controller = _$controller_;

        spyOn(gameTypeService, "gameTypes").and.callFake(function () {
            var deferred = $q.defer();
            deferred.resolve(["AFL", "Wheel Chair Rugby"]);
            return deferred.promise;
        });

        spyOn(groundService, "grounds").and.callFake(function () {
            var deferred = $q.defer();
            deferred.resolve(grounds);
            return deferred.promise;
        });
    }));

    it("fetches game types from the game type service", inject(function ($rootScope) {
        var $scope = {};

        var controller = $controller("setupController", { $scope: $scope });

        $rootScope.$apply();

        expect($scope.gameTypes).toEqual(["AFL", "Wheel Chair Rugby"]);
    }));

    it("fetches ground types from the ground service", inject(function ($rootScope) {
        var $scope = {};

        var controller = $controller("setupController", { $scope: $scope });

        $rootScope.$apply();

        expect($scope.grounds).toEqual(grounds);
    }));

    it("sets default ground when game type is chosen", function () {
        var $scope = {
            gameTypes: ["AFL"],
            gameType: "AFL",
            grounds: [{
                Name: "Pattersons",
                Type: "AFL"
            },
            {
                Name: "Wheel Chair Rugby",
                Type: "Wheel Chair Rugby"
            }]
        };
        var controller = $controller("setupController", { $scope: $scope });
        $scope.changeGameType("AFL");

        expect($scope.ground).toEqual({
            Name: "Pattersons",
            Type: "AFL"
        });
    });

    it("sets the game type when a ground is chosen", function () {
        var $scope = {
            grounds: [ {Name: "Wheel Chair Rugby", Type: "WheelChairRugby" }],
            gameTypes: ["AFL", "Wheel Chair Rugby"]
        };

        var controller = $controller("setupController", { $scope: $scope });
        $scope.ground = { Name: "Wheel Chair Rugby", Type: "WheelChairRugby" };
        $scope.changeGround();

        expect($scope.gameType).toEqual("Wheel Chair Rugby");
    });

    it("hides advanced settings by default", function () {
        var $scope = {};

        var controller = $controller("setupController", { $scope: $scope });

        expect($scope.showAdvancedSettings).toEqual(false);
    });

    it("shows advanced settings when button advanced settings button is pressed", function () {
        var $scope = {};

        var controller = $controller("setupController", { $scope: $scope });
        $scope.toggleAdvancedSettings();

        expect($scope.showAdvancedSettings).toEqual(true);
    })
});