/*global angular*/

(function () {
	'use strict';

	angular
		.module('App')
		.config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {
			/* Define routes ---------------------------------------------------------- */

			/* ApplicationController ------------------------------------------------- */
			$routeProvider
				.when('/error', {
					controller: 'ApplicationController',
					templateUrl: '/views/shared/error.html'
				});

			/* HomeController -------------------------------------------------------- */
			$routeProvider
				.when('/home', {
					controller: 'IndexController',
					templateUrl: '/views/home/index.html'
				});

			/* BudgetController ------------------------------------------------------ */
			$routeProvider
				.when('/budget', {
					controller: 'ListBudgetController',
					templateUrl: '/views/budget/list.html'
				})
				.when('/budget/new', {
					controller: 'NewBudgetController',
					templateUrl: '/views/budget/new.html'
				})
				.when('/budget/delete', {
					controller: 'DeleteBudgetController',
					templateUrl: '/views/budget/delete.html'
				});

			/* FundSourceController -------------------------------------------------- */
			$routeProvider
				.when('/fundsource', {
					controller: 'ListFundSourceController',
					templateUrl: '/views/fundsource/list.html'
				})
				.when('/fundsource/new', {
					controller: 'NewFundSourceController',
					templateUrl: '/views/fundsource/new.html'
				})
				.when('/fundsource/delete', {
					controller: 'DeleteFundSourceController',
					templateUrl: '/views/fundsource/delete.html'
				});

			$routeProvider.otherwise({ redirectTo: '/error' });

			/* Configure location ---------------------------------------------------- */
			$locationProvider.html5Mode({
				enabled: false,
				requireBase: false
			});
		}]);
}());