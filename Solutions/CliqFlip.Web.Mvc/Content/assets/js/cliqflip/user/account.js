var _formLocation = null;

function InitAccountSettings() {
	_formLocation = $("#frmAccountLocation");

	CliqFlip.Validate.ShowLocationRemoteLocationValidation(_formLocation);
}