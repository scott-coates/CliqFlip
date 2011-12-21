jQuery(function($) {
	var $ = jQuery;

	$(function() {
		var data = {
			items: [
				{ value: "21", name: "Mick Jagger" },
				{ value: "43", name: "Johnny Storm" },
				{ value: "46", name: "Richard Hatch" },
				{ value: "54", name: "Kelly Slater" },
				{ value: "55", name: "Rudy Hamilton" },
				{ value: "79", name: "Michael Jordan" }
			]
		};
		$("#interestSearch").autoSuggest(data.items, { selectedItemProp: "name", searchObjProps: "name" });
	});
});