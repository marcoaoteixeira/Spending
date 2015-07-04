/* Spending application.js
 * ================
 * Main JS application file for Spending. This file should be included in all
 * pages. It controls some layout options and implements exclusive Spending
 * plugins.
 */

'use strict';

// Make sure jQuery has been loaded before application.js
if (typeof jQuery === 'undefined') {
	throw new Error('Spending requires jQuery');
}

/* Application
 *
 * @type Object
 * @description $.Application is the main object for the application. It's used
 *              for implementing functions and options related to the
 *              application. Keeping everything wrapped in an object prevents
 *              conflict with other plugins and is a better way to organize the
 *              code.
 */
$.Application = {};

/* --------------------
 * - Application Options -
 * --------------------
 */
$.Application.options = {
	// Add slimscroll to navbar menus
	// This requires you to load the slimscroll plugin in every page before
	// application.js
	navbarMenuSlimscroll: true,
	navbarMenuSlimscrollWidth: '3px', // The width of the scroll bar
	navbarMenuHeight: '200px', // The height of the inner menu
	// General animation speed for JS animated elements such as box
	// collapse/expand and sidebar treeview slide up/down. This options accepts
	// an integer as milliseconds, 'fast', 'normal', or 'slow'
	animationSpeed: 500,
	// Sidebar push menu toggle button selector
	sidebarToggleSelector: '[data-toggle="offcanvas"]',
	// Activate sidebar push menu
	sidebarPushMenu: true,
	// Activate sidebar slimscroll if the fixed layout is set (requires
	// SlimScroll Plugin)
	sidebarSlimScroll: true,
	// Enable sidebar expand on hover effect for sidebar mini
	// This option is forced to true if both the fixed layout and sidebar mini
	// are used together
	sidebarExpandOnHover: false,
	// BoxRefresh Plugin
	enableBoxRefresh: true,
	// Bootstrap.js tooltip
	enableBSToppltip: true,
	BSTooltipSelector: '[data-toggle="tooltip"]',
	// Enable Fast Click. Fastclick.js creates a more native touch experience
	// with touch devices. If you choose to enable the plugin, make sure you
	// load the script before Application's application.js
	enableFastclick: true,
	// Control Sidebar Options
	enableControlSidebar: true,
	controlSidebarOptions: {
		// Which button should trigger the open/close event
		toggleBtnSelector: '[data-toggle="control-sidebar"]',
		// The sidebar selector
		selector: '.control-sidebar',
		// Enable slide over content
		slide: true
	},
	// Box Widget Plugin. Enable this plugin to allow boxes to be collapsed
	// and/or removed
	enableBoxWidget: true,
	// Box Widget plugin options
	boxWidgetOptions: {
		boxWidgetIcons: {
			// Collapse icon
			collapse: 'fa-minus',
			// Open icon
			open: 'fa-plus',
			// Remove icon
			remove: 'fa-times'
		},
		boxWidgetSelectors: {
			// Remove button selector
			remove: '[data-widget="remove"]',
			// Collapse button selector
			collapse: '[data-widget="collapse"]'
		}
	},
	// Direct Chat plugin options
	directChat: {
		// Enable direct chat by default
		enable: true,
		// The button to open and close the chat contacts pane
		contactToggleSelector: '[data-widget="chat-pane-toggle"]'
	},
	// Define the set of colors to use globally around the website
	colors: {
		lightBlue: '#3c8dbc',
		red: '#f56954',
		green: '#00a65a',
		aqua: '#00c0ef',
		yellow: '#f39c12',
		blue: '#0073b7',
		navy: '#001F3F',
		teal: '#39CCCC',
		olive: '#3D9970',
		lime: '#01FF70',
		orange: '#FF851B',
		fuchsia: '#F012BE',
		purple: '#8E24AA',
		maroon: '#D81B60',
		black: '#222222',
		gray: '#d2d6de'
	},
	// The standard screen sizes that bootstrap uses.
	// If you change these in the variables.less file, change them here too.
	screenSizes: {
		xs: 480,
		sm: 768,
		md: 992,
		lg: 1200
	}
};

/* ------------------
 * - Implementation -
 * ------------------
 * The next block of code implements Application's functions and plugins as
 * specified by the options above.
 */
$(function () {
	// Extend options if external options exist
	if (typeof ApplicationOptions !== 'undefined') {
		$.extend(true
			, $.Application.options
			, ApplicationOptions);
	}

	// Easy access to options
	var options = $.Application.options;

	// Set up the object
	_init();

	// Activate the layout maker
	$.Application.layout.activate();

	// Enable sidebar tree view controls
	$.Application.tree('.sidebar');

	// Enable control sidebar
	if (options.enableControlSidebar) {
		$.Application.controlSidebar.activate();
	}

	// Add slimscroll to navbar dropdown
	if (options.navbarMenuSlimscroll && typeof $.fn.slimscroll !== 'undefined') {
		$('.navbar .menu').slimscroll({
			height: options.navbarMenuHeight,
			alwaysVisible: false,
			size: options.navbarMenuSlimscrollWidth
		}).css('width', '100%');
	}

	// Activate sidebar push menu
	if (options.sidebarPushMenu) {
		$.Application.pushMenu.activate(options.sidebarToggleSelector);
	}

	// Activate Bootstrap tooltip
	if (options.enableBSToppltip) {
		$('body').tooltip({
			selector: options.BSTooltipSelector
		});
	}

	// Activate box widget
	if (options.enableBoxWidget) {
		$.Application.boxWidget.activate();
	}

	// Activate fast click
	if (options.enableFastclick && typeof FastClick != 'undefined') {
		FastClick.attach(document.body);
	}

	// Activate direct chat widget
	if (options.directChat.enable) {
		$(options.directChat.contactToggleSelector).on('click', function () {
			$(this).parents('.direct-chat').first().toggleClass('direct-chat-contacts-open');
		});
	}

	/*
	 * INITIALIZE BUTTON TOGGLE
	 * ------------------------
	 */
	$('.btn-group[data-toggle="btn-toggle"]').each(function () {
		var group = $(this);
		$(this).find('.btn').on('click', function (e) {
			group.find('.btn.active').removeClass('active');
			$(this).addClass('active');
			e.preventDefault();
		});

	});
});

/* ----------------------------------
 * - Initialize the Application Object -
 * ----------------------------------
 * All Application functions are implemented below.
 */
function _init() {

	/* Layout
	 * ======
	 * Fixes the layout height in case min-height fails.
	 *
	 * @type Object
	 * @usage $.Application.layout.activate()
	 *        $.Application.layout.fix()
	 *        $.Application.layout.fixSidebar()
	 */
	$.Application.layout = {
		activate: function () {
			var self = this;
			self.fix();
			self.fixSidebar();
			$(window, '.wrapper').resize(function () {
				self.fix();
				self.fixSidebar();
			});
		},
		fix: function () {
			// Get window height and the wrapper height
			var neg = $('.main-header').outerHeight() + $('.main-footer').outerHeight();
			var window_height = $(window).height();
			var sidebar_height = $('.sidebar').height();
			// Set the min-height of the content and sidebar based on the the
			// height of the document.
			if (!$('body').hasClass('fixed')) {
				var postSetWidth;
				if (window_height >= sidebar_height) {
					$('.content-wrapper, .right-side').css('min-height', window_height - neg);
					postSetWidth = window_height - neg;
				} else {
					$('.content-wrapper, .right-side').css('min-height', sidebar_height);
					postSetWidth = sidebar_height;
				}

				// Fix for the control sidebar height
				var controlSidebar = $($.Application.options.controlSidebarOptions.selector);
				if (typeof controlSidebar !== 'undefined') {
					if (controlSidebar.height() > postSetWidth)
						$('.content-wrapper, .right-side').css('min-height', controlSidebar.height());
				}
			} else {
				$('.content-wrapper, .right-side').css('min-height', window_height - $('.main-footer').outerHeight());
			}
		},
		fixSidebar: function () {
			// Make sure the body tag has the .fixed class
			if (!$('body').hasClass('fixed')) {
				if (typeof $.fn.slimScroll != 'undefined') {
					$('.sidebar').slimScroll({ destroy: true }).height('auto');
				}
				return;
			} else if (typeof $.fn.slimScroll == 'undefined' && console) {
				console.error('Error: the fixed layout requires the slimscroll plugin!');
			}
			// Enable slimscroll for fixed layout
			if ($.Application.options.sidebarSlimScroll) {
				if (typeof $.fn.slimScroll != 'undefined') {
					// Destroy if it exists
					$('.sidebar').slimScroll({ destroy: true }).height('auto');
					// Add slimscroll
					$('.sidebar').slimscroll({
						height: ($(window).height() - $('.main-header').height()) + 'px',
						color: 'rgba(0,0,0,0.2)',
						size: '3px'
					});
				}
			}
		}
	};

	/* PushMenu()
	 * ==========
	 * Adds the push menu functionality to the sidebar.
	 *
	 * @type Function
	 * @usage: $.Application.pushMenu('[data-toggle="offcanvas"]')
	 */
	$.Application.pushMenu = {
		activate: function (toggleBtn) {
			// Get the screen sizes
			var screenSizes = $.Application.options.screenSizes;

			// Enable sidebar toggle
			$(toggleBtn).on('click', function (e) {
				e.preventDefault();

				// Enable sidebar push menu
				if ($(window).width() > (screenSizes.sm - 1)) {
					if ($('body').hasClass('sidebar-collapse')) {
						$('body').removeClass('sidebar-collapse').trigger('expanded.pushMenu');
					} else {
						$('body').addClass('sidebar-collapse').trigger('collapsed.pushMenu');
					}
				}
				// Handle sidebar push menu for small screens
				else {
					if ($('body').hasClass('sidebar-open')) {
						$('body').removeClass('sidebar-open').removeClass('sidebar-collapse').trigger('collapsed.pushMenu');
					} else {
						$('body').addClass('sidebar-open').trigger('expanded.pushMenu');
					}
				}
			});

			$('.content-wrapper').click(function () {
				// Enable hide menu when clicking on the content-wrapper on
				// small screens
				if ($(window).width() <= (screenSizes.sm - 1) && $('body').hasClass('sidebar-open')) {
					$('body').removeClass('sidebar-open');
				}
			});

			// Enable expand on hover for sidebar mini
			if ($.Application.options.sidebarExpandOnHover
					|| ($('body').hasClass('fixed')
							&& $('body').hasClass('sidebar-mini'))) {
				this.expandOnHover();
			}
		},
		expandOnHover: function () {
			var self = this;
			var screenWidth = $.Application.options.screenSizes.sm - 1;
			// Expand sidebar on hover
			$('.main-sidebar').hover(function () {
				if ($('body').hasClass('sidebar-mini')
						&& $('body').hasClass('sidebar-collapse')
						&& $(window).width() > screenWidth) {
					self.expand();
				}
			}, function () {
				if ($('body').hasClass('sidebar-mini')
						&& $('body').hasClass('sidebar-expanded-on-hover')
						&& $(window).width() > screenWidth) {
					self.collapse();
				}
			});
		},
		expand: function () {
			$('body').removeClass('sidebar-collapse').addClass('sidebar-expanded-on-hover');
		},
		collapse: function () {
			if ($('body').hasClass('sidebar-expanded-on-hover')) {
				$('body').removeClass('sidebar-expanded-on-hover').addClass('sidebar-collapse');
			}
		}
	};

	/* Tree()
	 * ======
	 * Converts the sidebar into a multilevel tree view menu.
	 *
	 * @type Function
	 * @Usage: $.Application.tree('.sidebar')
	 */
	$.Application.tree = function (menu) {
		var self = this;
		var animationSpeed = $.Application.options.animationSpeed;
		$('li a', $(menu)).on('click', function (e) {
			// Get the clicked link and the next element
			var $this = $(this);
			var checkElement = $this.next();

			// Check if the next element is a menu and is visible
			if ((checkElement.is('.treeview-menu')) && (checkElement.is(':visible'))) {
				// Close the menu
				checkElement.slideUp(animationSpeed, function () {
					checkElement.removeClass('menu-open');
					// Fix the layout in case the sidebar stretches over the
					// height of the window self.layout.fix();
				});
				checkElement.parent('li').removeClass('active');
			}
			// If the menu is not visible
			else if ((checkElement.is('.treeview-menu')) && (!checkElement.is(':visible'))) {
				// Get the parent menu
				var parent = $this.parents('ul').first();
				// Close all open menus within the parent
				var ul = parent.find('ul:visible').slideUp(animationSpeed);
				// Remove the menu-open class from the parent
				ul.removeClass('menu-open');
				// Get the parent li
				var parent_li = $this.parent('li');

				// Open the target menu and add the menu-open class
				checkElement.slideDown(animationSpeed, function () {
					// Add the class active to the parent li
					checkElement.addClass('menu-open');
					parent.find('li.active').removeClass('active');
					parent_li.addClass('active');
					// Fix the layout in case the sidebar stretches over the height of the window
					self.layout.fix();
				});
			}
			// If this isn't a link, prevent the page from being redirected
			if (checkElement.is('.treeview-menu')) {
				e.preventDefault();
			}
		});
	};

	/* ControlSidebar
	 * ==============
	 * Adds functionality to the right sidebar
	 *
	 * @type Object
	 * @usage $.Application.controlSidebar.activate(options)
	 */
	$.Application.controlSidebar = {
		//instantiate the object
		activate: function () {
			// Get the object
			var self = this;
			// Update options
			var options = $.Application.options.controlSidebarOptions;
			// Get the sidebar
			var sidebar = $(options.selector);
			// The toggle button
			var btn = $(options.toggleBtnSelector);

			// Listen to the click event
			btn.on('click', function (e) {
				e.preventDefault();
				// If the sidebar is not open
				if (!sidebar.hasClass('control-sidebar-open')
						&& !$('body').hasClass('control-sidebar-open')) {
					// Open the sidebar
					self.open(sidebar, options.slide);
				} else {
					self.close(sidebar, options.slide);
				}
			});

			// If the body has a boxed layout, fix the sidebar bg position
			var bg = $('.control-sidebar-bg');
			self._fix(bg);

			// If the body has a fixed layout, make the control sidebar fixed      
			if ($('body').hasClass('fixed')) {
				self._fixForFixed(sidebar);
			} else {
				// If the content height is less than the sidebar's height, force max
				// height
				if ($('.content-wrapper, .right-side').height() < sidebar.height()) {
					self._fixForContent(sidebar);
				}
			}
		},
		// Open the control sidebar
		open: function (sidebar, slide) {
			var self = this;
			// Slide over content
			if (slide) {
				sidebar.addClass('control-sidebar-open');
			} else {
				// Push the content by adding the open class to the body instead of
				// the sidebar itself
				$('body').addClass('control-sidebar-open');
			}
		},
		// Close the control sidebar
		close: function (sidebar, slide) {
			if (slide) {
				sidebar.removeClass('control-sidebar-open');
			} else {
				$('body').removeClass('control-sidebar-open');
			}
		},
		_fix: function (sidebar) {
			var self = this;
			if ($('body').hasClass('layout-boxed')) {
				sidebar.css('position', 'absolute');
				sidebar.height($('.wrapper').height());
				$(window).resize(function () {
					self._fix(sidebar);
				});
			} else {
				sidebar.css({
					'position': 'fixed',
					'height': 'auto'
				});
			}
		},
		_fixForFixed: function (sidebar) {
			sidebar.css({
				'position': 'fixed',
				'max-height': '100%',
				'overflow': 'auto',
				'padding-bottom': '50px'
			});
		},
		_fixForContent: function (sidebar) {
			$('.content-wrapper, .right-side').css('min-height', sidebar.height());
		}
	};

	/* BoxWidget
	 * =========
	 * BoxWidget is a plugin to handle collapsing and removing boxes from the
	 * screen.
	 *
	 * @type Object
	 * @usage $.Application.boxWidget.activate()
	 *        Set all your options in the main $.Application.options object
	 */
	$.Application.boxWidget = {
		selectors: $.Application.options.boxWidgetOptions.boxWidgetSelectors,
		icons: $.Application.options.boxWidgetOptions.boxWidgetIcons,
		animationSpeed: $.Application.options.animationSpeed,
		activate: function (box) {
			var self = this;
			if (!box) {
				box = document; // Activate all boxes per default
			}
			// Listen for collapse event triggers
			$(box).find(self.selectors.collapse).on('click', function (e) {
				e.preventDefault();
				self.collapse($(this));
			});

			// Listen for remove event triggers
			$(box).find(self.selectors.remove).on('click', function (e) {
				e.preventDefault();
				self.remove($(this));
			});
		},
		collapse: function (element) {
			var self = this;
			// Find the box parent
			var box = element.parents('.box').first();
			// Find the body and the footer
			var box_content = box.find('> .box-body, > .box-footer, > form  >.box-body, > form > .box-footer');
			if (!box.hasClass('collapsed-box')) {
				// Convert minus into plus
				element.children(':first')
					.removeClass(self.icons.collapse)
					.addClass(self.icons.open);
				// Hide the content
				box_content.slideUp(self.animationSpeed, function () {
					box.addClass('collapsed-box');
				});
			} else {
				// Convert plus into minus
				element.children(':first')
						.removeClass(self.icons.open)
						.addClass(self.icons.collapse);
				// Show the content
				box_content.slideDown(self.animationSpeed, function () {
					box.removeClass('collapsed-box');
				});
			}
		},
		remove: function (element) {
			// Find the box parent
			element.parents('.box').first().slideUp(this.animationSpeed);
		}
	};
}

/* ------------------
 * - Custom Plugins -
 * ------------------
 * All custom plugins are defined below.
 */

/*
 * BOX REFRESH BUTTON
 * ------------------
 * This is a custom plugin to use with the component BOX. It allows you to add
 * a refresh button to the box. It converts the box's state to a loading state.
 *
 * @type plugin
 * @usage $('#box-widget').boxRefresh( options );
 */
(function ($) {
	$.fn.boxRefresh = function (options) {
		// Render options
		var settings = $.extend({
			// Refresh button selector
			trigger: '.refresh-btn',
			// File source to be loaded (e.g: ajax/src.php)
			source: '',
			// Callbacks
			onLoadStart: function (box) {
			}, // Right after the button has been clicked
			onLoadDone: function (box) {
			} // When the source has been loaded

		}, options);

		// The overlay
		var overlay = $('<div class="overlay"><div class="fa fa-refresh fa-spin"></div></div>');

		return this.each(function () {
			// If a source is specified
			if (settings.source === '') {
				if (console) {
					console.log('Please specify a source first - boxRefresh()');
				}
				return;
			}
			// The box
			var box = $(this);
			// The button
			var button = box.find(settings.trigger).first();

			// On trigger click
			button.on('click', function (e) {
				e.preventDefault();
				// Add loading overlay
				start(box);

				// Perform ajax call
				box.find('.box-body').load(settings.source, function () {
					done(box);
				});
			});
		});

		function start(box) {
			// Add overlay and loading img
			box.append(overlay);

			settings.onLoadStart.call(box);
		}

		function done(box) {
			// Remove overlay and loading img
			box.find(overlay).remove();

			settings.onLoadDone.call(box);
		}

	};

})(jQuery);

/*
 * EXPLICIT BOX ACTIVATION
 * -----------------------
 * This is a custom plugin to use with the component BOX. It allows you to
 * activate a box inserted in the DOM after the application.js was loaded.
 *
 * @type plugin
 * @usage $('#box-widget').activateBox();
 */
(function ($) {
	$.fn.activateBox = function () {
		$.Application.boxWidget.activate(this);
	};
})(jQuery);

/*
 * TODO LIST CUSTOM PLUGIN
 * -----------------------
 * This plugin depends on iCheck plugin for checkbox and radio inputs
 *
 * @type plugin
 * @usage $('#todo-widget').todolist( options );
 */
(function ($) {
	$.fn.todolist = function (options) {
		// Render options
		var settings = $.extend({
			// When the user checks the input
			onCheck: function (element) { },
			// When the user unchecks the input
			onUncheck: function (element) { }
		}, options);

		return this.each(function () {
			if (typeof $.fn.iCheck != 'undefined') {
				$('input', this).on('ifChecked', function (event) {
					var element = $(this).parents('li').first();
					element.toggleClass('done');
					settings.onCheck.call(element);
				});

				$('input', this).on('ifUnchecked', function (event) {
					var element = $(this).parents('li').first();
					element.toggleClass('done');
					settings.onUncheck.call(element);
				});
			} else {
				$('input', this).on('change', function (event) {
					var element = $(this).parents('li').first();
					element.toggleClass('done');
					settings.onCheck.call(element);
				});
			}
		});
	};
}(jQuery));