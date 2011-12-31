function InitAutoSuggest(data) {
	$("#interestSearch").autoSuggest(data,
		{
			asHtmlID: "post",
			selectedValuesProp: "Id",
			selectedItemProp: "Name",
			searchObjProps: "Name",
			startText: "Type in some things you like",
			neverSubmit: true,
			selectionAdded: function (elem) { elem.fadeTo("slow", 0.33); }

		});
}