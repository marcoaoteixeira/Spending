/*global angular*/

(function () {
	'use strict';

	angular
		.module('App')
		.run(['$rootScope', 'TranslationService', function ($rootScope, translationService) {
			translationService.defineTranslationDelegate($rootScope);
		}]);
}());