var _formLocation = null;

function InitAccountSettings() {
	_formLocation = $("#frmAccountLocation");

	cliqFlip.Validate.ShowLocationRemoteLocationValidation(_formLocation);
}