function InitAutoSuggest(data) {
	$("#interestSearch").autoSuggest(data, {selectedValuesProp:"Id", selectedItemProp: "Name", searchObjProps: "Name", startText: "Type in some things you like" });
}