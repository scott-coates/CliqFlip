var _canEdit = false;
var _addPhotoDialog;
var _addInterestDialog;

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

		_addInterestDialog = $("#add-interest").dialog({
			autoOpen: false,
			modal: true,
			width: 600,
			height:600
		});

		$(".user-interest-add-photo").click(function () {
			$("#userInterestId").val($(this).attr('value'));
			_addPhotoDialog.dialog("open");
		});

		$(".user-interest-remove").click(function() {
			return RemoveInterest();
		});

		$("#add-interest-button").click(function () {
			_addInterestDialog.dialog("open");
		});
	}
}

function InitShowImages(makeDefaultUrl, removeImageUrl) {
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
			title: function() {
				var title = $(this).attr('title');

				if (_canEdit) {
					title = title
						+ " <a href=" + makeDefaultUrl + "/?imageId=" + $(this).attr('value') + " title='make default'>make default</a>"
							+ " <a href=" + removeImageUrl + "/?imageId=" + $(this).attr('value')
								+ " title='remove image' onclick='return RemoveImage();'>remove image</a>";
				}

				return title;
			}
		});
}

function AddInterestToVisitorProfile() {
	$(".user-interest-add-single").click(function () {
		return AddInterest();
	});
}

function RemoveImage() {
	return confirm("Are you sure you want to remove this image? This cannot be undone.");
}

function RemoveInterest() {
	return confirm("Are you sure you want to remove this interest? This cannot be undone.");
}

function AddInterest() {
	return confirm("Are you sure you want to add this interest to your profile?");
}