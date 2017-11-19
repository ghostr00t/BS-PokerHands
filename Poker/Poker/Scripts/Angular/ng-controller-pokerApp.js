var pokerApp = angular.module('pokerApp', []);
pokerApp.controller('PokerController', function ($scope, $http) {

	$scope.cards = [];
	$scope.hand = "";

	$scope.GetCards = function () {
		
		$http.get("/CardDecks/GetCards").then(function (response) {
			$scope.cards = angular.fromJson(response.data.Item1);
			$scope.hand = response.data.Item2;
		})		
	}


});