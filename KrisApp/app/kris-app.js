(function () {

    "use strict";

    angular.module("kris-app", ["ngRoute"])
    .config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "calcController",
            controllerAs: "vm",
            templateUrl: "app/calc/calcView.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" });

    });

})();