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
				var lang        = ($cookies.lang || 'pt-br'),
					translation = null;

				$http.get('api/translation/?cultureName=' + lang, { cache: true })
					.success(function (data, status, headers, config) {
						translation = data;
						/* inject translation into $scope */
						scope.T = function (message, args) {
							var result = '',
								json = (typeof message === 'object');

							if (translation === null) {
								return json ? message.id : message;
							}

							result = json
								? translation[message.scope][message.id] || message.id
								: translation[''][message] || message;

							return args !== undefined
								? String.format(result, args)
								: result;
						};
					});
			};
			/* ----------------------------------------------------------- Public Methods */
		}]);
}());