//petsController.js
(function () {

	"use strict";

	angular.module("app-pets")
		.controller("fostersController", petsController);

	function fostersController($http) {

		var vm = this;

		vm.fosters = [];

		vm.errorMessage = "";
		vm.isBusy = true;

		$http.get("/api/fosters")
			.then(function (response) {
				//success
				angular.copy(response.data, vm.fosters);
			},
			function () {
				//failure
				vm.errorMessage = "Failed to load data: " + error;
			})
			.finally(function () {
				vm.isBusy = false;
			});
	};
})();