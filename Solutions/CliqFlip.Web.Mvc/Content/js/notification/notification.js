var _cookieName = null;
var _notificationId = null;
var _notification = null;

function InitNotification(cookieName, notificationId) {
	_cookieName = _cookieName;
	_notificationId = notificationId;
	setInterval(SetupNotifications, 1000);
}

function SetupNotifications() {
	$('body').animate({
		'padding-top': '50px'
	}, 1000, function () {
		_notification = $('#notification').notify({ type: 'sticky', onClose: Persist });

		//make every link popout
		$("a:not(.close)", _notification).attr('target', '_blank');
	});
}

function Persist() {
	$.cookie(_cookieName, _notificationId, { expires: 365, path: "/" });
	setTimeout(ShowBody, 500);
}

function ShowBody() {
	$('body').animate({
		'padding-top': '0'
	}, 500);
}