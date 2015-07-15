/*global angular*/

(function () {
	'use strict';

	angular
		.module('Application')
		.run(['$rootScope', 'TranslationService', function ($rootScope, translationService) {
			translationService.defineTranslationDelegate($rootScope);
		}]);
}());