//the textbox the user types their interest into
var interestTextBox = null;
//the div holding the category questions
var divInterestCategory = null;

//the create form
var formProfileCreate = null;

//the list of added interests
var interestsList = null;

var tmplNewInterest = null;
var tmplStepIndicator = null;
var steps = null;

var interestNames = new Array();

function initUserCreate() {
    var selectedCssClass = "dark-blue";
    var unselectedCssClass = "blue";
    
    function generateIndicators() {
        //each fieldset is a step in in the wizard.
        //The <strong> tag will serve all the title
        //add an indicator for each step in the wizard
        formProfileCreate.find("fieldset").each(function (index, element) {
            var title = $(this).find("strong").text();
            var stepId = $(this).attr("id");
            var indicator = tmplStepIndicator.tmpl({
                StepNumber: index + 1,
                StepTitle: title,
                StepId: stepId
            });
            if (index == 0) {
                indicator.find("a").addClass(selectedCssClass).removeClass(unselectedCssClass);
            }
            indicator.appendTo(steps);
        });

        //bind the indicators to move to the step whenever it's clicked
        steps.find("a").click(function (event) {
            event.preventDefault();
            var step = $(this).attr("href").replace("#", "");
            formProfileCreate.formwizard("show", step);
        });
    }

    function createFormWizard() {
        var onStepShown = function (event, data) {
                
                //Whenever a step is show, update the indicators to reflect the step
                //their looking at.
                //get all the indicators with the selectedCssClass and remove the class
                steps.find("a[class~='" + selectedCssClass + "']").removeClass(selectedCssClass).addClass(unselectedCssClass);

                //mark the currently selected step
                steps.find("a[href='#" + data.currentStep + "']").addClass(selectedCssClass).removeClass(unselectedCssClass);
            };

        var formWizardOptions = {
            disableUIStyles: true,
            inDuration: 100,
            outDuration: 100
        };

        formProfileCreate.formwizard(formWizardOptions).bind("step_shown", onStepShown);

    }

    function createAutoComplete() {
        interestTextBox.autocomplete({
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
                onInterestAdded(ui.item);
                this.value = "";
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>").data("item.autocomplete", item).append("<a>" + item.Name + "</a>").appendTo(ul);
        };

        //whenever the link 'a.remove-interest' is clicked
        //it should remove the interest from the list of added interests
        interestsList.on("click", "a.remove-interest", function (event) {
            event.preventDefault();
            var li = $(this) //the anchor clicked
            .parents("li"); //get the closest parent li
            var interest = li.data("item");

            onInterestRemoved(interest);

            li.fadeOut(function () { //fade it out
                li.remove(); //then remove it
            });
        });
    }

    function onInterestAdded(interest) {

        //if an interest with the same Interest has been added
        if ($.inArray(interest.Name.toLowerCase(), interestNames) >= 0) {
            return;
        }

        interestNames.push(interest.Name.toLowerCase());
        var data = $.extend({}, {
            Index: interestsList.find("li").length
        }, interest);
        var newItem = tmplNewInterest.tmpl(data);
        newItem.data("item", interest)
        newItem.appendTo(interestsList);
        newItem.fadeIn();
    }

    function onInterestRemoved(interest) {
        interestNames = $.grep(interestNames, function (e) {
            return e !== interest.Name.toLowerCase();
        });
    }

    $(document).ready(function () {
        //bind all the elements we need
        interestTextBox = $("#interestName");
        divInterestCategory = $("#divInterestCategory");
        interestsList = $("#interestsList");
        tmplNewInterest = $("#tmplNewInterest");
        tmplStepIndicator = $("#tmplStepIndicator");
        steps = $("#steps");


        //find the user create form
        formProfileCreate = $("#formProfileCreate");

        createFormWizard();
        generateIndicators();
        createAutoComplete();
    });
}