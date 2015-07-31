/* Include Scripts ********************************************************* */
var gulp       = require('gulp');
var concat     = require('gulp-concat');
var uglify     = require('gulp-uglify');
var del        = require('del');
var minifyCSS  = require('gulp-minify-css');
var copy       = require('gulp-copy');
var bower      = require('gulp-bower');
var sourcemaps = require('gulp-sourcemaps');
/* ********************************************************* Include Scripts */

var config = {
	/* AngularJS Bundle ********************************************************* */
	angularsrc: [
		'bower_components/angular/angular.min.js',
		'bower_components/angular-i18n/angular-locale_pt-br.js',
		'bower_components/angular-animate/angular-animate.min.js',
		'bower_components/angular-aria/angular-aria.min.js',
		'bower_components/angular-cookies/angular-cookies.min.js',
		'bower_components/angular-loader/angular-loader.min.js',
		'bower_components/angular-messages/angular-messages.min.js',
		'bower_components/angular-resource/angular-resource.min.js',
		'bower_components/angular-route/angular-route.min.js',
		'bower_components/angular-sanitize/angular-sanitize.min.js',
		'bower_components/angular-touch/angular-touch.min.js'
	],
	angularbundle: 'Scripts/angular-bundle.min.js',
	/* ********************************************************* AngularJS Bundle */

	/* Bootstrap Bundle ********************************************************* */
	bootstrapsrc: [
		'bower_components/bootstrap/dist/js/bootstrap.min.js'
	],
	bootstrapbundle: 'Scripts/bootstrap-bundle.min.js',
	/* ********************************************************* Bootstrap Bundle */

	/* jQuery Bundle ************************************************************ */
	jquerysrc: [
		'bower_components/jquery/dist/jquery.min.js'
	],
	jquerybundle: 'Scripts/jquery-bundle.min.js',
	/* ************************************************************ jQuery Bundle */

	/* Lodash Bundle ************************************************************ */
	lodashsrc: [
		'bower_components/lodash/lodash.min.js'
	],
	lodashbundle: 'Scripts/lodash-bundle.min.js',
	/* ************************************************************ Lodash Bundle */

	/* RespondJS Bundle ********************************************************* */
	respondjssrc: [
		'bower_components/respondJS/dest/respond.min.js'
	],
	respondjsbundle: 'Scripts/respondjs-bundle.min.js',
	/* ********************************************************* RespondJS Bundle */

	/* Utils Bundle ************************************************************* */
	utilssrc: [
		'Scripts/strings.js'
	],
	utilsbundle: 'Scripts/utils-bundle.min.js',
	/* ************************************************************* Utils Bundle */
	
	/* Application Bundle ******************************************************* */
	applicationsrc: [
		'App/application.js',
		'App/config.js',
		'App/startup.js',
		
		'App/controllers/budget-controller.js',
		'App/controllers/category-controller.js',
		'App/controllers/credit-controller.js',
		'App/controllers/debit-controller.js',
		'App/controllers/home-controller.js',

		'App/services/translation-service.js'
	],
	applicationbundle: 'App/application-bundle.min.js',
	/* ******************************************************* Application Bundle */

	/* Bootstrap CSS and Fonts ************************************************** */
	bootstrapcss: 'bower_components/bootstrap/dist/css/bootstrap.min.css',
	boostrapfonts: 'bower_components/bootstrap/dist/fonts/*.*',
	/* ************************************************** Bootstrap CSS and Fonts */

	/* Application CSS and Fonts ************************************************ */
	appcss: 'Content/main.css',
	fontsout: 'Content/dist/fonts',
	cssout: 'Content/dist/css',
	/* ************************************************ Application CSS and Fonts */
}

/* Synchronously delete the output script file(s) ************************** */
gulp.task('clean-vendor-scripts', function (callback) {
	del([
		config.angularbundle,
		config.bootstrapbundle,
		config.jquerybundle,
		config.lodashbundle,
		config.respondjsbundle,
		config.utilsbundle,
		config.applicationbundle
	], callback);
});
/* ************************** Synchronously delete the output script file(s) */

/* Create an AngularJS bundled file **************************************** */
gulp.task('angular-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.angularsrc)
		.pipe(concat('angular-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* **************************************** Create an AngularJS bundled file */

/* Create a Bootstrap bundled file ***************************************** */
gulp.task('bootstrap-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.bootstrapsrc)
		.pipe(sourcemaps.init())
		.pipe(concat('bootstrap-bundle.min.js'))
		.pipe(sourcemaps.write('maps'))
		.pipe(gulp.dest('Scripts'));
});
/* ***************************************** Create a Bootstrap bundled file */

/* Create a jQuery bundled file ******************************************** */
gulp.task('jquery-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.jquerysrc)
		.pipe(concat('jquery-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* ******************************************** Create a jQuery bundled file */

/* Create a Lodash bundled file ******************************************** */
gulp.task('lodash-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.lodashsrc)
		.pipe(concat('lodash-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* ******************************************** Create a Lodash bundled file */

/* Create a RespondJS bundled file ***************************************** */
gulp.task('respondjs-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.respondjssrc)
		.pipe(concat('respondjs-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* ***************************************** Create a RespondJS bundled file */

/* Create a Utils bundled file ********************************************* */
gulp.task('utils-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.utilssrc)
		.pipe(concat('utils-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* ********************************************* Create a Utils bundled file */

/* Create a Application bundled file *************************************** */
gulp.task('application-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.applicationsrc)
		.pipe(concat('application-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});
/* *************************************** Create a Application bundled file */

/* Synchronously delete the output style files (css / fonts) *************** */
gulp.task('clean-styles', function (callback) {
	del([
		config.fontsout,
		config.cssout
	], callback);
});
/* *************** Synchronously delete the output style files (css / fonts) */

/* Combine and the vendor files from bower into bundles (output to the Scripts
   folder ****************************************************************** */
gulp.task('vendor-scripts', [
	'angular-bundle',
	'bootstrap-bundle',
	'jquery-bundle',
	'lodash-bundle',
	'respondjs-bundle',
	'utils-bundle',
	'application-bundle'
], function () {

});
/* ************************************************************************* */

/* Build style files ******************************************************* */
gulp.task('css', ['clean-styles', 'bower-restore'], function () {
	return gulp.src([
		config.bootstrapcss,
		config.appcss
	]).pipe(concat('app.css'))
		.pipe(gulp.dest(config.cssout))
		.pipe(minifyCSS())
		.pipe(concat('app.min.css'))
		.pipe(gulp.dest(config.cssout));
});
/* ******************************************************* Build style files */

/* Copy fonts to output folder ********************************************* */
gulp.task('fonts', ['clean-styles', 'bower-restore'], function () {
	return gulp.src(config.boostrapfonts)
		.pipe(gulp.dest(config.fontsout));
});
/* ********************************************* Copy fonts to output folder */

/* Combine and minify css files and output fonts *************************** */
gulp.task('styles', ['css', 'fonts'], function () {

});
/* *************************** Combine and minify css files and output fonts */

/* Restore all bower packages ********************************************** */
gulp.task('bower-restore', function () {
	return bower();
});
/* ********************************************** Restore all bower packages */

/* Set a default tasks ***************************************************** */
gulp.task('default', ['vendor-scripts', 'styles'], function () {

});
/* ***************************************************** Set a default tasks */