/* global angular */

(function () {
	'use strict';

	/* Define controllers ------------------------------------------------------- */
	angular
		.module('Application')
		.controller('ApplicationController', ApplicationController);
	/* ------------------------------------------------------- Define controllers */

	/* Application Controller --------------------------------------------------- */
	ApplicationController.$inject = ['$scope'];

	function ApplicationController($scope) {
        $scope.sidebarMenuItemActive = '';
        
		$scope.activeSideBarMenuItem = function (menu) {
            $scope.sidebarMenuItemActive = menu;
        };
	};
	/* --------------------------------------------------- Application Controller */
}());