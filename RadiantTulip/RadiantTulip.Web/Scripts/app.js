var radiantTulip = angular.module("radiant", ["ngRoute"]);

radiantTulip.config(function ($routeProvider) {
    $routeProvider.
        when("/", {
            templateUrl: "Views/Setup.html",
            controller: "setupController"
        }).
        otherwise({
            redirectTo: "/"
        });
});