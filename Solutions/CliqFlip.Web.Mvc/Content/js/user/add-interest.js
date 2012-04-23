//the textbox the user types their interest into
var _interestTextBox = null;

//the div holding the category questions
var _divInterestCategory = null;

//the list of added interests
var _interestsList = null;

var _tmplNewInterest = null;


var _interestNames = [];
var _optionsListHtml = [];
function InitAddInterest() {
	//bind all the elements we need
	_interestTextBox = $("#interestName");
	_divInterestCategory = $("#divInterestCategory");
	_interestsList = $("#interestsList");
	_tmplNewInterest = $("#tmplNewInterest");


	//http://stackoverflow.com/a/1683041 - i was inspired by this answer
	//get the interests, create the options and store the options html that will be used
    //everytime an unknown interest is added
	$.getJSON("/Interest/GetMainCategoryInterests", function (json) {
	    //create an array that will hold the html for each individual option
	    //and the default option.
        //NOTE: The value must be set to empty or a validation error occurs
	    var tempList = ["<option value=''>Choose...</option>"];

	    for (var i = 0; i < json.length; i++) {
	        tempList.push("<option value='" + json[i].Value + "'>" + json[i].Text + "</option>");
	    }

	    _optionsListHtml = tempList.join('');
	});

	CreateAutoComplete();

	SetupEnterKey();
}

function SetupEnterKey() {
	var interestName = $("#interestName");
	interestName.keydown(function (event) {
	    var autoComplete = interestName.data("autocomplete");
	    // ',' key pressed
	    if (event.keyCode == 188 && $.trim(interestName.val()).length > 0) {
	        var activeMenuItem = autoComplete.menu.active;
	        OnInterestAdded(activeMenuItem.data("item.autocomplete"));
	        interestName.val("");
	        autoComplete.close();
	        return false;
	    }
	    return true;
	});
}

function CreateAutoComplete() {
    _interestTextBox.autocomplete({
        source: function (request, response) {
            // delegate back to autocomplete, but extract the last term
            var term = request.term;

            var addToList = true;

            //make a call to the Interest search
            //If the item the user has typed does not have an exact match
            //it should add it to the end of the list by pushing a new json object into the Interest array
            //Then let autocomplete do it's thing
            $.getJSON("/Search/Interest?input=" + term, function (data) {

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
        select: function (event, ui) {
            OnInterestAdded(ui.item);
            this.value = "";
            return false;
        },
        autoFocus: true //select the first element on the list
    }).data("autocomplete")._renderItem = function (ul, item) {
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

    //if an interest with the same name already is in the interestsNames array
    //dont add it. It's a duplicate
	if ($.inArray(interest.Name.toLowerCase(), _interestNames) >= 0) {
		return;
	}

    //this is a unique interest add it to the list of interets
    _interestNames.push(interest.Name.toLowerCase());

    //create the data object we will pass to template
    //add an index value
	var data = $.extend({ }, {
		Index: _interestsList.find("li").length
	}, interest);

    //get the data bound template
	var newItem = _tmplNewInterest.tmpl(data);
	newItem.data("item", interest);
    
    //get the empty and only select list
	var selectList = newItem.find("select");

    //add all the main category interests to the list
	selectList.html(_optionsListHtml);

    //add it to the DOM and show it
	newItem.appendTo(_interestsList);
	newItem.fadeIn();
}