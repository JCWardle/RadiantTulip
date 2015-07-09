/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/angular-route.js" />
/// <reference path="../Scripts/app.js" />
/// <reference path="../Scripts/Services/gameTypeService.js" />

describe('game type service', function () {
    var service, $httpBackend;
    var url = "http://localhost:9001/GroundType";

    beforeEach(module("radiant"));
    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get("$httpBackend");

        $httpBackend.when("GET", url)
            .respond(["AFL", "Wheel Chair Rugby"], "200");
        service = $injector.get('gameTypeService');
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('fetches all game types', function () {
        var result;
        $httpBackend.expectGET(url);
        service.gameTypes().then(function (d) { result = d });
        $httpBackend.flush();
        expect(result.length).toEqual(2);
        expect(result[0]).toEqual("AFL");
        expect(result[1]).toEqual("Wheel Chair Rugby");
    });
});