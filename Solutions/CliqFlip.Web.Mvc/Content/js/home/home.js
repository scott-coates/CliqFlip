var subjects = null;

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
			centrex: 200,
			centrey: 170,
			min_font_size: 10,
			max_font_size: 16,
			zoom: 90,
			init_motion_x: 2,
			init_motion_y: 2,
			rotate_factor: 2,
			fps:30
		});

	$("#interest-tag-cloud ul").show();

		$("#interest-tag-cloud a").click(function () {
			var tagValue = $(this).attr('value');
			var tagName = this.innerText;
			var tagValueToAdd = { Name: tagName, Id: tagValue };
			subjects.addNewItem(tagValueToAdd);
		});
}