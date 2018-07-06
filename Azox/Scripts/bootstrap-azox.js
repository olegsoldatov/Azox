/*!
 * Bootstrap Azox v1.0.0.0
 * Copyright 2018 Soldata
 */

// Back To Top.
$(function () {
	$(window).scroll(function () {
		if ($(this).scrollTop() >= 450) {
			$('[data-toggle=toTop]').fadeIn();
		} else {
			$('[data-toggle=toTop]').fadeOut();
		}
	});

	$('[data-toggle=toTop]').click(function () {
		$('body,html').animate({ scrollTop: 0 }, 400);
		return false;
	});
});

// Scroll To.
$(document).ready(function () {
	$('[data-toggle=scrollTo]').click(function () {
		var scroll_el = $(this).attr('href');
		if ($(scroll_el).length !== 0) {
			$('html, body').animate({ scrollTop: $(scroll_el).offset().top }, 500);
		}
		return false;
	});
});
