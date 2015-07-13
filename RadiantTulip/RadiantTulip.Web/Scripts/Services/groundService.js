angular.module("radiant")
.factory("groundService", function ($http) {
    var apiUrl = "http://localhost:9001/Ground";
    var service = {
        grounds: function () {
            var promise = $http.get(apiUrl).then(function (response) {
                return response.data;
            });
            return promise;
        }
    }
    return service;
});