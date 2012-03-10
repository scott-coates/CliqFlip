//the textbox the user types their interest into
var _interestTextBox = null;

//the div holding the category questions
var _divInterestCategory = null;

//the list of added interests
var _interestsList = null;

var _tmplNewInterest = null;


var _interestNames = [];

function InitAddInterest() {
	//bind all the elements we need
	_interestTextBox = $("#interestName");
	_divInterestCategory = $("#divInterestCategory");
	_interestsList = $("#interestsList");
	_tmplNewInterest = $("#tmplNewInterest");

	CreateAutoComplete();
}

function CreateAutoComplete() {
	_interestTextBox.autocomplete({
		source: function(request, response) {
			// delegate back to autocomplete, but extract the last term
			var term = request.term;

			var addToList = true;

			//make a call to the Interest search
			//If the item the user has typed does not have an exact match
			//it should add it to the end of the list by pushing a new json object into the Interest array
			//Then let autocomplete do it's thing
			$.getJSON("/Search/Interest?input=" + term, function(data) {

				//if the item the user is searching for is in the list
				//it should not be added at the end
				for (var counter = 0; counter < data.length; counter++) {
					if (data[counter].Name.toLowerCase() == term.toLowerCase()) {
						addToList = false;
					}
				}

				if (addToList) {
					data.push({
						Name: term,
						Id: 0
					});
				}
				response(data);
			});
		},
		select: function(event, ui) {
			OnInterestAdded(ui.item);
			this.value = "";
			return false;
		}
	}).data("autocomplete")._renderItem = function(ul, item) {
		return $("<li></li>").data("item.autocomplete", item).append("<a>" + item.Name + "</a>").appendTo(ul);
	};

	//whenever the link 'a.remove-interest' is clicked
	//it should remove the interest from the list of added interests
	_interestsList.on("click", "a.remove-interest", function(event) {
		event.preventDefault();
		var li = $(this) //the anchor clicked
			.parents("li"); //get the closest parent li
		var interest = li.data("item");

		OnInterestRemoved(interest);

		li.fadeOut(function() { //fade it out
			li.remove(); //then remove it
		});
	});
}


function OnInterestRemoved(interest) {
	_interestNames = $.grep(_interestNames, function(e) {
		return e !== interest.Name.toLowerCase();
	});
}

function GenerateInterestCategoryVisibility(interest) {
	if (interest.Id != 0) {
		return "visibility: hidden;";
	}
	return '';
}

function OnInterestAdded(interest) {

	//if an interest with the same Interest has been added
	if ($.inArray(interest.Name.toLowerCase(), _interestNames) >= 0) {
		return;
	}

	_interestNames.push(interest.Name.toLowerCase());
	var data = $.extend({ }, {
		Index: _interestsList.find("li").length
	}, interest);
	var newItem = _tmplNewInterest.tmpl(data);
	newItem.data("item", interest);
	newItem.appendTo(_interestsList);
	newItem.fadeIn();
}