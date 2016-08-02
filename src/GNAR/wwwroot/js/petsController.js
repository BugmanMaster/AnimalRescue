//petsController.js
(function () {

	"use strict";

	angular.module("app-pets")
		.controller("petsController", petsController);

	function petsController($http) {

		var vm = this;

		vm.pets = [];

		vm.newPet = {};

		vm.errorMessage = "";
		vm.isBusy = true;

		$http.get("/api/pets")
			.then(function (response) {
				//success
				angular.copy(response.data, vm.pets);
			},
			function () {
				//failure
				vm.errorMessage = "Failed to load data: " + error;
			})
			.finally(function () {
				vm.isBusy = false;
			});

		vm.addPet = function () {
			vm.isBusy = true;
			vm.errorMessage = "";

			$http.post("/api/pets", vm.newPet)
				.then(function (response) {
					//success
					vm.pets.push(response.data);
					vm.newPet = {};
				},
				function () {
					//failure
					vm.errorMessage = "Failed to save new pet: " + error;
				})
				.finally(function () {
					vm.isBusy = false;
				});
		};
	}

})();