﻿var _canEdit = false;
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

		$(".user-interest-title-add-photo").click(function(parameters) {
			$("#userInterestId").val($(this).attr('value'));
			_addPhotoDialog.dialog("open");
		});
	}
}

function InitShowImages(makeDefaultUrl) {
	$('.intereset-slider').anythingSlider(
		{
			hashTags: false,
			resizeContents: false,
			theme: "default1",
			buildStartStop: false
		})
		.show()
		.find('.panel:not(.cloned) a') // ignore the cloned panels
		.colorbox({
			width: '90%',
			height: '90%',
			preloading: false,
			title: function () {
				return $(this).attr('title')+
					" <a href=" + makeDefaultUrl + "/?imageId=" + $(this).attr('value') + " title='make default'>make default</a>";
			}
		});
}