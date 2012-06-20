var cliqFlip = (function (cliqFlip) {
	cliqFlip.Validate = {};

	cliqFlip.Validate.AddClassRules = function (className, validateRuleObj) {
		jQuery.validator.addClassRules(className, validateRuleObj);
	};

	cliqFlip.Validate.ShowLocationRemoteLocationValidation = function(form) {
		var validator = form.data("validator");
		var locationRemoteValidation = validator.settings.rules.Location.remote;

		//only on blur
		validator.settings.onkeyup = false;

		//the remote validation uses $.ajax to make it's request
		//Add a callback so once it's complete, we can display the location
		//the location will be in the header
		locationRemoteValidation.complete = function (jqXHR) {
			var span = $("#location-found", form);
			if (jqXHR.responseText === "true") {
				var location = jqXHR.getResponseHeader("location-found");
				span.html(location);
			}
			else {
				span.html("");
			}
		};
	};

	return cliqFlip;
} (cliqFlip || {}));