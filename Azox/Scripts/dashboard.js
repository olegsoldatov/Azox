/*!
 * Dashboard v1.1.0.0
 * Copyright 2017 Soldata
 */

if (typeof jQuery === 'undefined') {
	throw new Error('Dashboard\'s JavaScript requires jQuery');
}

$("[aria-expanded]").click(function () {
	var controlsId = "#" + $(this).attr("aria-controls");
	if ($(this).attr("aria-expanded") === "true") {
		$(this).attr("aria-expanded", "false");
		$(controlsId).attr("aria-hidden", "true");
	} else {
		$(this).attr("aria-expanded", "true");
		$(controlsId).attr("aria-hidden", "false");
	}
});

$(".sidebar > button[data-toggle=collapse]").click(function () {
	$("body").addClass("collapsed");
});

$(".sidebar > button[data-toggle=expand]").click(function () {
	$("body").removeClass("collapsed");
});

$(".table th").change(function () {
	$(".table td [type=checkbox]").prop("checked", $(".table th [type=checkbox]").prop("checked"));
});

$("[data-wrap=table-actions] a").click(function () {
	var guids = $(".table td [type=checkbox]:checked");
	var url = $(this).data("url");
	var params = $.param($(guids));
	if (params !== "") {
		$.post(url, params, function (data) {
			document.location = data.redirect;
		});
		return false;
	}
});

// To Top.
$(function () {
	$('.content').scroll(function () {
		if ($(this).scrollTop() >= 450) {
			$('[data-toggle=toTop]').fadeIn();
		} else {
			$('[data-toggle=toTop]').fadeOut();
		}
	});

	$('[data-toggle=toTop]').click(function () {
		$('.content').animate({ scrollTop: 0 }, 400);
		return false;
	});
});

// Datepicker.
$('.input-group.date').datepicker({
	format: "dd.mm.yyyy",
	language: "ru",
	autoclose: true,
	todayHighlight: true
});

