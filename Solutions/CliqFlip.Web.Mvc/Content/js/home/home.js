function InitAutoSuggest(data) {
	var some = $("#interestSearch").autoSuggest(data,
		{
			asHtmlID: "post",
			selectedValuesProp: "Id",
			selectedItemProp: "Name",
			searchObjProps: "Name",
			startText: "Type in some things you like",
			neverSubmit: true,
			selectionAdded: function (elem) { elem.fadeTo("slow", 0.33); }

		});
	some.addNewItem({ Name: 'Autos', Id: '2' });
}