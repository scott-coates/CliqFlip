var _canEdit = false;
var _addPhotoDialog;
var _addVideoDialog;
var _addLinkDialog;
var _addInterestDialog;

function InitInterests(canEdit) {
	_canEdit = canEdit;
}

function InitSavePost() {
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

function InitShowPost(makeDefaultUrl, removePostUrl) {
    $('.intereset-slider').anythingSlider(
		{
		    hashTags: false,
		    resizeContents: false,
		    theme: "default1",
		    buildStartStop: false,
		    buildNavigation: false,
		    onBeforeInitialize: function (event, slider) {
		        //I could of added the rel attribute to the anchor on the server side
		        //but I didn't want to add it to the view model
		        //so any li that is in the list should be grouped together
		        var listItems = slider.$el.find("li");
		        listItems.find("a").attr("rel", new Date().getTime());
		    }
		})
		.show()
		.find('.panel:not(.cloned) a') // ignore the cloned panels
		.colorbox({
			current: "Post {current} of {total}",
		    iframe: function () {
		        //if the medium is an image, don't show it in an iframe
		        //everything else show in an iframe
		        return $(this).attr("medium") != "Image";
		    },
		    width: '90%',
		    height: '90%',
		    preloading: false,
		    title: function () {
		        var title = $(this).attr('title');

		        if (_canEdit) {
		            title = title
						+ " <a href=" + makeDefaultUrl + "/?postId=" + $(this).attr('value') + " title='make default'>make default</a>"
							+ " <a href=" + removePostUrl + "/?postId=" + $(this).attr('value')
								+ " title='remove post' onclick='return RemovePost();'>remove post</a>";
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

function RemovePost() {
	return confirm("Are you sure you want to remove this post? This cannot be undone.");
}

function RemoveInterest() {
	return confirm("Are you sure you want to remove this interest? This cannot be undone.");
}

function AddInterest() {
	return confirm("Are you sure you want to add this interest to your profile?");
}