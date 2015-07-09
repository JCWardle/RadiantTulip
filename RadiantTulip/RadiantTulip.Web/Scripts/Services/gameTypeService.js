angular.module("radiant")
.factory("gameTypeService", function ($http) {
    var apiUrl = "http://localhost:9001/GroundType";
    var service = {
        gameTypes: function () {
            var promise = $http.get(apiUrl).then(function (response) {
                return response.data;
            });
            return promise;
        }
    }
    return service;
});