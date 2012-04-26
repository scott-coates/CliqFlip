var _canEdit = false;
var _addPhotoDialog;
var _addVideoDialog;
var _addLinkDialog;
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

		_addVideoDialog = $("#add-video").dialog({
			autoOpen: false,
			modal: true,
			width: 430
		});

		_addLinkDialog = $("#add-link").dialog({
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
			$("#userInterestIdPhoto").val($(this).attr('value'));
			_addPhotoDialog.dialog("open");
		});

		$(".user-interest-add-video").click(function () {
			$("#userInterestIdVideo").val($(this).attr('value'));
			_addVideoDialog.dialog("open");
		});

		$(".user-interest-add-link").click(function () {
			$("#userInterestIdLink").val($(this).attr('value'));
			_addLinkDialog.dialog("open");
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
		    buildStartStop: false,
            buildNavigation: false
//		    onInitialized: function (event, slider) {
//		        var elements = slider.$currentPage.find("p, a strong, a div");
//		        elements.each(function () {
//		            var el = $(this);
//		            if (!el.data("dotdotdot")) {
//		                el.dotdotdot();
//		            }
//		        });
//		        //slider.$currentPage.find("p, a strong, a div").dotdotdot();
//		    },
//		    onSlideInit: function (event, slider) {
//		        //alert("hey");
//		        //slider.$currentPage.find("p").dotdotdot();
//		        //slider.$targetPage.find("p, a strong, a div").dotdotdot();
//		        var elements = slider.$targetPage.find("p, a strong, a div");
//		        elements.each(function () {
//		            var el = $(this);
//		            if (!el.data("dotdotdot")) {
//		                el.dotdotdot();
//		            }
//		        });
//		    }
		})
		.show()
		.find('.panel:not(.cloned) a') // ignore the cloned panels
		.colorbox({
		    iframe: function () {
		        //if the content is an image, don't show it in an iframe
		        //everything else show in an iframe
		        return $("input[name='type']", this).val() != "image";
		    },
		    width: '90%',
		    height: '90%',
		    preloading: false,
		    title: function () {
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