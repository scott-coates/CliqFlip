function InitAutoSuggest(data) {
	$("#interestSearch").autoSuggest(data.items, { selectedItemProp: "name", searchObjProps: "name", startText: "Type in some things you like" });
}