var subjects = null;


function FormatList(data, elem) {
	var systemAliasPrefix = data.SystemAlias.substring(0, 2);

	$(elem).html(data.Name);

	if (systemAliasPrefix === "-1") {
		var item = $("em", elem);
		item.css('background', 'red');
	}

	return elem;
}

function SelectionAdded(elem, data) {
	//TODO: use the #as-values-search_values elem to see if -1 instead of modify source code
	
	var systemAliasPrefix = data.SystemAlias.substring(0, 2);

	if (systemAliasPrefix === "-1") {
		var item = $(elem);
		item.css('color', 'red');
	}

	return elem;
}

function InitAutoSuggest(searchUrl) {
	subjects = $("#interestSearch").autoSuggest(searchUrl,
		{
			asHtmlID: "search_values",
			selectedValuesProp: "SystemAlias",
			selectedItemProp: "Name",
			searchObjProps: "SystemAlias",
			queryParam: "input",
			minChars: 2,
			startText: "Type in some things you like",
			neverSubmit: true,
			formatList: FormatList,
			selectionAdded: SelectionAdded
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

function ChangeSearchQueryParamName() {
	$('form').submit(function (parameters) {
		$("#as-values-search_values").attr('name', 'q');
	});
}