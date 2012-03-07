var _canEdit = false;

function InitInterests(canEdit) {
	_canEdit = canEdit;
}

function InitSaveImages() {
	if (_canEdit) {
		$("#add-photo").dialog();
	}
}