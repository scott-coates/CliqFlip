//the create form
var _formProfileCreate = null;

var _tmplStepIndicator = null;
var _steps = null;

var _selectedCssClass = "dark-blue";
var _unselectedCssClass = "blue";

function InitUserCreate() {
	//bind all the elements we need
	_tmplStepIndicator = $("#tmplStepIndicator");
	_steps = $("#steps");

	//find the user create form
	_formProfileCreate = $("#formProfileCreate");

	CreateFormWizard();
	GenerateIndicators();
}

function GenerateIndicators() {
	//each fieldset is a step in in the wizard.
	//The <strong> tag will serve all the title
	//add an indicator for each step in the wizard
	_formProfileCreate.find("fieldset").each(function(index, element) {
		var title = $(this).find("strong").text();
		var stepId = $(this).attr("id");
		var indicator = _tmplStepIndicator.tmpl({
			StepNumber: index + 1,
			StepTitle: title,
			StepId: stepId
		});
		if (index == 0) {
			indicator.find("a").addClass(_selectedCssClass).removeClass(_unselectedCssClass);
		}
		indicator.appendTo(_steps);
	});

	//bind the indicators to move to the step whenever it's clicked
	_steps.find("a").click(function(event) {
		event.preventDefault();
		var step = $(this).attr("href").replace("#", "");
		_formProfileCreate.formwizard("show", step);
	});
}

function CreateFormWizard() {

	var formWizardOptions = {
		disableUIStyles: true,
		inDuration: 100,
		outDuration: 100
	};

	_formProfileCreate.formwizard(formWizardOptions).bind("step_shown", OnStepShown);
	_formProfileCreate.data('validator').settings.submitHandler = function(form) {
		var userInterests = $(".userInterestValue", $(form));
		if (userInterests.length > 0) {
			form.submit();
		}
		else {
			_formProfileCreate.formwizard("show", "stepInterests");
			$("#userInterestEmptyError").fadeIn(1000);
		}
	};
}

function OnStepShown(event, data) {

	//Whenever a step is show, update the indicators to reflect the step
	//their looking at.
	//get all the indicators with the selectedCssClass and remove the class
	//TODO: remove the contains selector
	_steps.find("a[class~='" + _selectedCssClass + "']").removeClass(_selectedCssClass).addClass(_unselectedCssClass);

	//mark the currently selected step
	_steps.find("a[href='#" + data.currentStep + "']").addClass(_selectedCssClass).removeClass(_unselectedCssClass);
}