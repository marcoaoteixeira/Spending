/* Include Scripts ********************************************************* */
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var minifyCSS = require('gulp-minify-css');
var copy = require('gulp-copy');
var bower = require('gulp-bower');
var sourcemaps = require('gulp-sourcemaps');
/* ********************************************************* Include Scripts */

var config = {
	/* AngularJS Bundle **************************************************** */
	angularsrc: [
		'bower_components/angular/angular.min.js',
		'bower_components/angular-animate/angular-animate.min.js',
		'bower_components/angular-aria/angular-aria.min.js',
		//'bower_components/angular-bootstrap-ui/angular-bootstrap-ui.min.js',
		'bower_components/angular-cookies/angular-cookies.min.js',
		'bower_components/angular-i18n/angular-locale_pt-br.js',
		'bower_components/angular-resource/angular-resource.min.js',
		'bower_components/angular-route/angular-route.min.js',
		'bower_components/angular-sanitize/angular-sanitize.min.js',
		'bower_components/angular-touch/angular-touch.min.js'
	],
	angularbundle: 'Scripts/angular-bundle.min.js',
	/* **************************************************** AngularJS Bundle */

	/* Bootstrap Bundle **************************************************** */
	bootstrapsrc: [
		'bower_components/bootstrap/dist/js/bootstrap.min.js'
	],
	bootstrapbundle: 'Scripts/bootstrap-bundle.min.js',
	/* **************************************************** Bootstrap Bundle */

	/* jQuery Bundle ******************************************************* */
	jquerysrc: [
		'bower_components/jquery/dist/jquery.min.js'
	],
	jquerybundle: 'Scripts/jquery-bundle.min.js',
	/* ******************************************************* jQuery Bundle */

	/* Lodash Bundle ******************************************************* */
	lodashsrc: [
		'bower_components/lodash/lodash.min.js'
	],
	lodashbundle: 'Scripts/lodash-bundle.min.js',
	/* ******************************************************* Lodash Bundle */

	/* RespondJS Bundle **************************************************** */
	respondjssrc: [
		'bower_components/respondJS/dest/respond.min.js'
	],
	respondjsbundle: 'Scripts/respondjs-bundle.min.js',
	/* **************************************************** RespondJS Bundle */

	/* Utils Bundle ******************************************************** */
	utilssrc: [
		'Scripts/strings.js'
	],
	utilsbundle: 'Scripts/utils-bundle.min.js',
	/* ******************************************************** Utils Bundle */
	
	/* Application Bundle ************************************************** */
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
	/* ************************************************** Application Bundle */

	//Bootstrap CSS and Fonts
	bootstrapcss: 'bower_components/bootstrap/dist/css/bootstrap.min.css',
	boostrapfonts: 'bower_components/bootstrap/dist/fonts/*.*',

	appcss: 'Content/Site.css',
	fontsout: 'Content/dist/fonts',
	cssout: 'Content/dist/css'
}

// Synchronously delete the output script file(s)
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

//Create a jquery bundled file
gulp.task('jquery-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.jquerysrc)
		.pipe(concat('jquery-bundle.min.js'))
		.pipe(gulp.dest('Scripts'));
});

//Create a bootstrap bundled file
gulp.task('bootstrap-bundle', ['clean-vendor-scripts', 'bower-restore'], function () {
	return gulp.src(config.bootstrapsrc)
		.pipe(sourcemaps.init())
		.pipe(concat('bootstrap-bundle.min.js'))
		.pipe(sourcemaps.write('maps'))
		.pipe(gulp.dest('Scripts'));
});

// Combine and the vendor files from bower into bundles (output to the Scripts folder)
gulp.task('vendor-scripts', ['jquery-bundle', 'bootstrap-bundle', 'modernizer'], function () {

});

// Synchronously delete the output style files (css / fonts)
gulp.task('clean-styles', function (callback) {
	del([
		config.fontsout,
		config.cssout
	], callback);
});

gulp.task('css', ['clean-styles', 'bower-restore'], function () {
	return gulp.src([config.bootstrapcss, config.appcss])
		.pipe(concat('app.css'))
		.pipe(gulp.dest(config.cssout))
		.pipe(minifyCSS())
		.pipe(concat('app.min.css'))
		.pipe(gulp.dest(config.cssout));
});

gulp.task('fonts', ['clean-styles', 'bower-restore'], function () {
	return gulp.src(config.boostrapfonts)
		.pipe(gulp.dest(config.fontsout));
});

// Combine and minify css files and output fonts
gulp.task('styles', ['css', 'fonts'], function () {

});

//Restore all bower packages
gulp.task('bower-restore', function () {
	return bower();
});

//Set a default tasks 
gulp.task('default', ['vendor-scripts', 'styles'], function () {

});