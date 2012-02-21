


function formLogin_OnSuccess(result, response) {
	//if the result does not contain the word failed
	if (result.indexOf("failed") == -1) {
		window.location.reload();
	}
}


function InitLogin() {
	var userLoginContainer = $("#user-login-container");
	userLoginContainer.keydown(function(event) {

		// ESCAPE key pressed
		if (event.keyCode == 27) {
			userLoginContainer.toggle();
		}
	});

	$("#user-login").click(function(event) {
		event.preventDefault();
		$(this).toggleClass("selected");
		userLoginContainer.toggle();
		$("#Username").focus();
	});

	$("#close-login").click(function(event) {
		event.preventDefault();
		userLoginContainer.hide();
	});
}