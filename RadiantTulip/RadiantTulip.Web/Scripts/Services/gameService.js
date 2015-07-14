angular.module("radiant")
.factory("gameService", function ($http) {
    var apiUrl = "http://localhost:9001/Game";
    var service = {
        createGame: function (ground, positionalFile) {
            var promise = $http( {
                method: "POST",
                url: apiUrl,
                headers: {
                    "Content-Type": "multipart/form-data"
                },
                transformRequest: function (data, headersGetter) {
                    var formData = new FormData();
                    angular.forEach(data, function (value, key) {
                        formData.append(key, value);
                    });

                    var headers = headersGetter();
                    delete headers['Content-Type'];

                    return formData;
                },
                data: { positionalFile: positionalFile, ground: ground}
            })
            .then(function (response) {
                return response.data;
            });
            return promise;
        }
    }
    return service;
});