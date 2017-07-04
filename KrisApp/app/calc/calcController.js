(function () {

    "use strict";

    angular.module("kris-app")
        .controller("calcController", calcController);

    function calcController($http) {

        var vm = this;

        vm.results = [];
        vm.startupZusAmount = 487.90;
        vm.normalZusAmount = 1109.89;
        vm.netto = 10000;
        vm.selectedZus = "";
        vm.rateType = "month";
        vm.hoursCount = 160;
        vm.hourRate = 80;

        vm.isBusy = true;

        vm.calcB2b = function () {

            vm.isBusy = true;

            var isLowZus = vm.selectedZus == "startupZus";

            var zusAmount = isLowZus ? vm.startupZusAmount : vm.normalZusAmount;

            var isPerHour = vm.rateType == "hour";

            var nettoAmount = vm.netto;

            if (isPerHour) {

                nettoAmount = vm.hoursCount * vm.hourRate;
                console.log("hour: " + vm.hoursCount + " rate: " + vm.hourRate + " netto: " + nettoAmount);
            }

            console.log(vm.cleanIncome);

            $http.post("/api/b2b", { nettoAmount: nettoAmount, zusAmount: zusAmount})
                    .then(function (response) {
                        // success
                        console.log(response);
                        vm.cleanIncome = response.data;
                        console.log(vm.cleanIncome);

                        vm.results.push({
                            rate: isPerHour ? vm.hourRate + ' zł /' + vm.hoursCount + ' h' : vm.netto + ' zł',
                            rateType: isPerHour ? "Za godzinę" : "Miesięcznie",
                            cleanIncome: vm.cleanIncome,
                            zus: isLowZus ? "Obniżony" : "Normalny"
                        });

                        vm.isBusy = false;

                    }, function (err) {
                        // error
                        vm.errorMessage = "Failed to save new trip";
                    })
                    .finally(function () {
                        vm.isBusy = false;
                    });
        };

        vm.clearResults = function () {
            vm.results = [];
        };

        vm.changeRate = function (value) {

            switch (value) {
                case "month":
                    vm.nettoDesc = "Za miesiąc:";
                    break;
                case "hour":
                    vm.nettoDesc = "Za godzinę:";
                    break;
                default:
                    vm.nettoDesc = "Inny:";
                    break;
            }
        }
    }
})();
