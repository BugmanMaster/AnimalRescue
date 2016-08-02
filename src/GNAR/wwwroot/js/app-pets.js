//app-pets.js
(function () {

	"use strict";

	angular.module("app-pets", ["simpleControls", "ngRoute"])
		.config(function ($routeProvider) {

			$routeProvider.when("/", {
				controller: "petsController",
				controllerAs: "vm",
				templateUrl: "/views/petsView.html"
			});

			$routeProvider.when("/edit", {
				controller: "petsEditController",
				controllerAs: "vm",
				templateUrl: "/views/petsEdit.html"
			});

			$routeProvider.when("/fosters", {
				controller: "fostersController",
				controllerAs: "vm",
				templateUrl: "/views/fostersView.html"
			});


			$routeProvider.otherwise({ redirectTo: "/" });
		});

})();