/*!
 * Dashboard v1.7.0.0
 * Copyright 2020 Soldata
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
$('input[data-role="datepicker"]').datepicker({
	format: "dd.mm.yyyy",
	language: "ru",
	autoclose: true,
	todayHighlight: true
});

// Filter form submit.
$("#filterForm").submit(function () {
	$(this).find(":input").filter(function () {
		return !this.value;
	}).attr("disabled", true);
	return true;
});

// Currency.
$(document).ready(function () {
	$("input[type=currency]").inputmask({ "alias": "decimal", "groupSeparator": " ", "radixPoint": ",", "digits": "2", "digitsOptional": false });
});

// Progress Status.
function getImportStatus(statusUrl, statusMessageId, startButtonId) {
	var interval = 300;
	var timerId = setTimeout(function go() {
		$.getJSON(statusUrl, function (data) {
			if (data.State === 0) {
				$(startButtonId).removeAttr("disabled");
				clearTimeout(timerId);
			} else if (data.State === 1) {
				$(startButtonId).attr("disabled", "disabled");
				setTimeout(go, interval);
			} else if (data.State === 2) {
				$(startButtonId).removeAttr("disabled");
				clearTimeout(timerId);
			}

			if (data.Message !== "") {
				$(statusMessageId).text(data.Message);
			}
		});
	}, interval);
}

// Delete Checked items.
function deleteCheckedItems(confirmText, url, guids) {
	if (confirm(confirmText)) {
		var params = $.param($(guids));
		if (params !== "") {
			$.post(url, params, function (data) {
				document.location = data.redirect;
			});
		}
	}
}
