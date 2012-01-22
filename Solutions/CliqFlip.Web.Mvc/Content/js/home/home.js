var interests = null;


function FormatList(data, elem) {
	var systemAliasPrefix = data.SystemAlias.substring(0, 2);


	if (systemAliasPrefix === "-1") {
		$(elem).html(data.Name + " does not exist. Register to add it or explore another interest.");
		$(elem).unbind('click');
		$(elem).css('background-image', 'none');//active class is applied and puts a small, awkward blob
		$(elem).click(function (event) {
			event.stopPropagation();
			return false;
		});
		elem.removeClass('as-result-item');
		elem.addClass('invalidKeywordSearch');
	} else {
		$(elem).html(data.Name);
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

	$(elem).click(function (event) {
		event.preventDefault();
	});

	return elem;
}

function InitAutoSuggest(searchUrl) {
	interests = $("#interestSearch").autoSuggest(searchUrl,
		{
			keyDelay: 600,
			asHtmlID: "search_values",
			selectedValuesProp: "Slug",
			selectedItemProp: "Name",
			searchObjProps: "Name,OriginalInput",
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

	$("#interest-tag-cloud a").click(function () {
		var tagValue = $(this).attr('value');
		var tagName = this.innerText;
		var tagValueToAdd = { Name: tagName, Id: tagValue };
		interests.addNewItem(tagValueToAdd);
	});
}

function ChangeSearchQueryParamName() {
	$('form').submit(function (parameters) {
		$("#as-values-search_values").attr('name', 'q');
	});
}