/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/Controllers/setupController.js" />

describe('setup window validation', function () {
    beforeEach(module('radiant'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    it('does stuff', function () {
        var $scope = {};
        var controller = $controller("setupController", { $scope: $scope });
        expect($scope.SomeShit).toEqual("Derp");
    });
});