/* global angular */

(function () {
	'use strict';

	angular
		.module('Application')
		.service('TranslationService', ['$cookies', '$resource', '$http', function ($cookies, $resource, $http) {
			/* Private Fields ----------------------------------------------------------- */
			var self = this;
			/* ----------------------------------------------------------- Private Fields */

			/* Public Methods ----------------------------------------------------------- */
			self.defineTranslationDelegate = function (scope) {
				scope.T = function (message, args) {
					return String.format(message, args);
				};
			};
			/* ----------------------------------------------------------- Public Methods */
		}]);
}());