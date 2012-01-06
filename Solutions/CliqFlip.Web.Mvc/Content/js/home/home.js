var subjects = null;
var hasDraggedCloud = false;

var tagCloudClick = function(evt) {
	if (evt.which == 1) {
		hasDraggedCloud = true;
	}
	evt.preventDefault();
	return false;
};

var hideSuggestTagCloud = function(evt) {
	if (evt.which == 1) {
		$("#suggest-tag-cloud").effect('slide', { direction: 'left', mode: 'hide' }, "fast");
		$("#interest-tag-cloud").unbind('mousedown', hideSuggestTagCloud);
	}

	evt.preventDefault();
	return false;
};

var showSuggestionIfTagCloudNotUsed = function() {
	if (!hasDraggedCloud) {
		$("#suggest-tag-cloud").effect('slide');
		$("#interest-tag-cloud").mousedown(hideSuggestTagCloud);
	}

	$("#interest-tag-cloud").unbind('mousedown', tagCloudClick);
};

function InitAutoSuggest(data) {
	subjects = $("#interestSearch").autoSuggest(data,
		{
			asHtmlID: "post",
			selectedValuesProp: "Id",
			selectedItemProp: "Name",
			searchObjProps: "Name",
			startText: "Type in some things you like",
			neverSubmit: true
		});
}

function InitTagSphere() {
	$("#interest-tag-cloud").tagcloud(
		{
			centrex: 180,
			centrey: 180,
			min_font_size: 10,
			max_font_size: 18,
			zoom: 110,
			rotate_factor: 1,
			fps: 30
		});

	$("#interest-tag-cloud ul").show();

	$("#interest-tag-cloud a").click(function() {
		var tagValue = $(this).attr('value');
		var tagName = this.innerText;
		var tagValueToAdd = { Name: tagName, Id: tagValue };
		subjects.addNewItem(tagValueToAdd);
	});
}

function InitSuggestTagCloud() {
	$("#interest-tag-cloud").mousedown(tagCloudClick);
	setTimeout(showSuggestionIfTagCloudNotUsed, 3000);
}