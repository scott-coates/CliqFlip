var _redirectPage;

function InitThankYou(redirectPage) {
	_redirectPage = redirectPage;
	if (window._gaq) {
		window._gaq.push(function() {
			alert(window._gaq.t);
			Redirect();
		});
	}
	else {
		setTimeout(Redirect, 2000);
	}
}

function Redirect() {
	window.location.href = _redirectPage;
}