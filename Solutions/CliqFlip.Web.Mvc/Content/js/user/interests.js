var _canEdit = false;
var _addPhotoDialog;

function InitInterests(canEdit) {
	_canEdit = canEdit;
}

function InitSaveImages() {
	if (_canEdit) {
		_addPhotoDialog = $("#add-photo").dialog({
			autoOpen: false,
			modal: true,
			width: 430
		});

		$(".user-interest-title-add-photo").click(function (parameters) {
			$("#userInterestId").val($(this).attr('value'));
			_addPhotoDialog.dialog("open");
		});
	}
}