(function () {

    "use strict";

    angular.module("kris-app")
        .controller("calcController", calcController);

    function calcController($http) {

        var vm = this;

        vm.results = [];
        vm.startupZusAmount = 487.90;
        vm.normalZusAmount = 1109.89;
        vm.netto = 0;
        vm.selectedZus = "";
        vm.rateType = "month";

        vm.monthRate = 12000;

        vm.hoursCount = 160;
        vm.hourRate = 80;

        vm.dayRate = 650;
        vm.daysCount = 20;

        vm.isBusy = false;

        vm.calcB2b = function () {

            vm.isBusy = true;

            var isLowZus = vm.selectedZus == "startupZus";

            var zusAmount = isLowZus ? vm.startupZusAmount : vm.normalZusAmount;

            var nettoAmount = _countNettoAmount(vm);

            $http.post("/api/b2b", { nettoAmount: nettoAmount, zusAmount: zusAmount })
                    .then(function (response) {
                        // success
                        console.log(response);
                        vm.cleanIncome = response.data;
                        console.log(vm.cleanIncome);

                        vm.results.push({
                            rate: _prepareRatePerPeriod(vm),
                            rateType: 'per ' + vm.rateType,
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
                    vm.nettoDesc = "Netto za miesiąc:";
                    break;
                case "day":
                    vm.nettoDesc = "Netto za dzień:";
                    break;
                case "hour":
                    vm.nettoDesc = "Netto za godzinę:";
                    break;
                default:
                    vm.nettoDesc = "Inny:";
                    break;
            }
        }

        function _countNettoAmount(model) {

            var amount = 0;

            switch (model.rateType) {
                case "month":
                    amount = model.monthRate;
                    break;
                case "day":
                    amount = model.dayRate * model.daysCount;
                    break;
                case "hour":
                    amount = model.hourRate * model.hoursCount;
                    break;
            }

            console.log("rateType: " + model.rateType + " netto: " + amount);

            return amount;
        }

        function _prepareRatePerPeriod(model) {

            var desc = "";

            switch (model.rateType) {
                case "month":
                    desc = model.monthRate + ' zł';
                    break;
                case "day":
                    desc = model.dayRate + ' zł / ' + model.daysCount + ' d';
                    break;
                case "hour":
                    desc = model.hourRate + ' zł / ' + model.hoursCount + ' h';
                    break;

            }

            return desc;
        }
    }
})();
