function InitAutoSuggest(data) {
	var some = $("#interestSearch").autoSuggest(data,
		{
			asHtmlID: "post",
			selectedValuesProp: "Id",
			selectedItemProp: "Name",
			searchObjProps: "Name",
			startText: "Type in some things you like",
			neverSubmit: true
		});
}