/*global angular*/

(function () {
	'use strict';

	/* Define controllers ************************************************** */
	angular
		.module('App')
		.controller('ListBudgetController', ListBudgetController)
		.controller('NewBudgetController', ListBudgetController);
	/* ************************************************** Define controllers */

	/* List Controller ***************************************************** */
	ListBudgetController.$inject = ['$scope', 'BudgetService'];

	function ListBudgetController($scope, budgetService) {
		/* Public Methods ************************************************** */
		$scope.getBudgets = function () {

		};
		/* ************************************************** Public Methods */
	};
	/* ***************************************************** List Controller */

	/* New Controller ****************************************************** */
	NewBudgetController.$inject = ['$scope', 'BudgetService'];

	function NewBudgetController($scope, BudgetService) {
		/* Public Fields *************************************************** */
		$scope.budget = {
			description: '',
			period: {
				month: 0,
				year: 0
			},
			items: []
		};
		/* *************************************************** Public Fields */

		/* Public Methods ************************************************** */
		$scope.addNewBudgetItem = function (budgetItem) {
			$scope.budget.items.push(angular.copy(budgetItem));
		};

		$scope.removeBudgetItem = function (idx) {
			if ($scope.budget.items[idx] !== undefined) {
				$scope.budget.items.splice(idx, 1);
			}
		};

		$scope.saveBudget = function () {
			budgetService
				.add(angular.copy($scope.budget))
				.success(function (data) { })
				.error(function () { });
		};
		/* ************************************************** Public Methods */
	};
	/* ****************************************************** New Controller */
}());