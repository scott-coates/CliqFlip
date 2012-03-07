var _canEdit = false;
var _addPhotoDialog;

function InitInterests(canEdit) {
	_canEdit = canEdit;
}

function InitSaveImages() {
	if (_canEdit) {
		_addPhotoDialog = $("#add-photo").dialog({
			autoOpen: false,
			modal: true
		});

		$(".user-interest-title-add-photo").click(function (parameters) {
			_addPhotoDialog.dialog("open");
		});
	}
}