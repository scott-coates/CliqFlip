var _tosDialog = null;

function InitTOS() {
	_tosDialog = $("#tos-dialog").dialog({
		autoOpen: false,
		modal: true,
		width: 730,
		height: 480
	});

	$("#site-tos").click(function (parameters) {
		_tosDialog.dialog("open");
	});
}