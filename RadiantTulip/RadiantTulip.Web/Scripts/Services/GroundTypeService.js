angular.module("radiant")
.factory("gameTypeService", function ($http) {
    var apiUrl = "http://localhost:9001/GroundType";
    var gameTypes = {};
    return {
        getData: function () {
            $http.get(apiUrl)
            .success(function (data, status, config, headers) {
                gameTypes = data;
            })
            .error(function () {

            });
        },
        gameTypes: function() { 
            return gameTypes;
        }
    };
});