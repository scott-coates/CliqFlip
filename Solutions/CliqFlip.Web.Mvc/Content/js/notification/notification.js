function SetupNotifications(parameters) {
	function persist() {
		$.cookie("@Model.NotificationCookie", "@Model.Id", { expires: 365, path: "/" });
		setTimeout(showBody, 500);
	}

	function showBody() {
		$('body').animate({
			'padding-top': '0'
		}, 500);
	}

	var notification = null;
	$('body').animate({
		'padding-top': '50px'
	}, 1000, function () {
		notification = $('#notification').notify({ type: 'sticky', onClose: persist });

		//make every link popout
		$("a:not(.close)", notification).attr('target', '_blank');
	});
}