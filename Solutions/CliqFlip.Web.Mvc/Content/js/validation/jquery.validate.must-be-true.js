jQuery.validator.addMethod("must_be_true", function (value, element, options) {
	return element.checked;
});

$.validator.unobtrusive.adapters.addBool("mustbetrue", "must_be_true"); 