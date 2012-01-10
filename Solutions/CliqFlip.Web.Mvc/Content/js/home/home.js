var subjects = null;

function InitAutoSuggest(searchUrl) {
	subjects = $("#interestSearch").autoSuggest(searchUrl,
		{
			selectedValuesProp: "SystemAlias",
			selectedItemProp: "Name",
			searchObjProps: "SystemAlias",
			queryParam: "input",
			minChars: 2,
			startText: "Type in some things you like",
			neverSubmit: true
		});
}

function InitTagCloud() {
	$("#interest-tag-cloud a").tagcloud(
		{
			size: { start: 12, end: 18, unit: 'px' },
			color: { start: '#176787', end: '#24282D' }
		});

	$("#interest-tag-cloud a").show();

	$("#interest-tag-cloud a").click(function() {
		var tagValue = $(this).attr('value');
		var tagName = this.innerText;
		var tagValueToAdd = { Name: tagName, Id: tagValue };
		subjects.addNewItem(tagValueToAdd);
	});
}