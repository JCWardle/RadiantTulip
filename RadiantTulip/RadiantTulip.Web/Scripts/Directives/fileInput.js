angular.module("radiant")
.directive("fileInput", function () {
    return {
        scope: {
            fileInput: "="
        },
        //restrict: "E",
        link: function (scope, element, attrs) {
            element.bind("change", function (event) {
                var file = event.target.files[0];
                scope.fileInput = file ? file : undefined;
                scope.$apply();
            });
        }
    }
});