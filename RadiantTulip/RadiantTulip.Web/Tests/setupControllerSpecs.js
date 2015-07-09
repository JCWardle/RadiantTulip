/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/angular-route.js" />
/// <reference path="../Scripts/app.js" />
/// <reference path="../Scripts/Controllers/setupController.js" />

describe("setup window", function () {
    beforeEach(module('radiant'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    it("fetches game types from the game type service", function () {
        var $scope = {};
        var mockService = { gameTypes: function() {}};
        spyOn(mockService, "gameTypes")
            .and.returnValue(["AFL", "Wheel Chair Rugby"]);

        var controller = $controller("setupController", { $scope: $scope, gameTypeService: mockService });

        expect(mockService.gameTypes).toHaveBeenCalled();
        expect($scope.gameTypes).toEqual(["AFL", "Wheel Chair Rugby"]);
    });
});