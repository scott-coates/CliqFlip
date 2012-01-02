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

	function fillAuto() {



		$("#post").val('Autos');
		var e = jQuery.Event("keydown");
		$("#post").focus();
//		$("#post").blur();
////		$("#post").keydown();
//		e.which = 9; // # Some key code value
//		$("#post").trigger(e);
//		$("#post").trigger(e);
//		$("#post").trigger(e);
//		e.which = 40;
//		$("#post").trigger(e);
	}

function tabIt() {

	var e = jQuery.Event("keydown");
	e.which = 9; // # Some key code value
	$("#post").trigger(e);
	$("#as-result-item-2").click();
}

function clickIt() {

//	var e = jQuery.Event("keydown");
//	e.which = 9; // # Some key code value
	$("#as-result-item-2").click();
}