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
			centrey: 85,
			min_font_size: 10,
			max_font_size: 16,
			zoom: 100,
			init_motion_x: 2,
			init_motion_y: 2,
			rotate_factor: 2
		});
}