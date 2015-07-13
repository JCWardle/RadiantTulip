/// <reference path="../Scripts/angular.js" />
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/angular-route.js" />
/// <reference path="../Scripts/app.js" />
/// <reference path="../Scripts/Services/groundService.js" />
/// <reference path="../Scripts/Services/gameTypeService.js" />

describe('ground service', function () {
    var service, $httpBackend;
    var url = "http://localhost:9001/GroundType";

    var response = [{
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

    beforeEach(module("radiant"));
    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get("$httpBackend");

        $httpBackend.when("GET", url)
            .respond(response, "200");
        service = $injector.get('gameTypeService');
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('fetches all grounds', function () {
        var result;
        $httpBackend.expectGET(url);
        service.gameTypes().then(function (d) { result = d });
        $httpBackend.flush();
        expect(result.length).toEqual(2);
        expect(result[0]).toEqual(response[0]);
        expect(result[1]).toEqual(response[1]);
    });
});