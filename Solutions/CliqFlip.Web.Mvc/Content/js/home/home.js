var subjects = null;



function FormatList(data, elem) {
	var systemAliasPrefix = data.SystemAlias.substring(0,2);
	$(elem).html(data.Name);
	if (systemAliasPrefix === "-1")
	{
		var f = $("em", elem);
		f.css('background', 'red');
	}


	return elem;
}

function InitAutoSuggest(searchUrl) {
	subjects = $("#interestSearch").autoSuggest(searchUrl,
		{
			selectedValuesProp: "SystemAlias",
			selectedItemProp: "Name",
			searchObjProps: "SystemAlias",
			queryParam: "input",
			minChars: 2,
			startText: "Type in some things you like",
			neverSubmit: true,
			formatList: FormatList
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