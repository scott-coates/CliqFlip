jQuery(function ($) {
	var $ = jQuery;

	$(function () {
		var data = {
			items: [
				{ value: "21", name: "Sports" },
				{ value: "43", name: "Entertainment" },
				{ value: "46", name: "Auto" },
				{ value: "54", name: "Shopping" },
				{ value: "55", name: "Books" },
				{ value: "79", name: "Aviation" }
			]
		};
		
		$("#interestSearch").autoSuggest(data.items, { selectedItemProp: "name", searchObjProps: "name", startText: "Type in some things you like" });
	});
});