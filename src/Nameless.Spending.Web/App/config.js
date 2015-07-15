/* global angular */

(function () {
	'use strict';

	angular
		.module('Application')
		.config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {
			/* ApplicationController ---------------------------------------------------- */
			$routeProvider
                .when('/', {
                    controller: 'DashboardController',
                    templateUrl: 'views/dashboard/charts.html'
                })
				.when('/error', {
					controller: 'ApplicationController',
					templateUrl: 'views/shared/error.html'
				});
			/* ---------------------------------------------------- ApplicationController */

			/* CategoryController ------------------------------------------------------- */
			$routeProvider
				.when('/categories', {
					controller: 'ListCategoryController',
					templateUrl: 'views/categories/list.html'
				});
			/* ------------------------------------------------------- CategoryController */
            
            /* DashboardController ------------------------------------------------------ */
			$routeProvider
				.when('/dashboard', {
					controller: 'DashboardController',
					templateUrl: 'views/dashboard/charts.html'
				});
			/* ------------------------------------------------------ DashboardController */
			
			$routeProvider.otherwise({ redirectTo: '/error' });

			/* Configure location provider ---------------------------------------------- */
			$locationProvider.html5Mode({
				enabled: false,
				requireBase: false
			});
			/* ---------------------------------------------- Configure location provider */
		}]);
}());