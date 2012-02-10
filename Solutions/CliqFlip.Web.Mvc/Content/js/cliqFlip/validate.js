var cliqFlip = (function(cliqFlip) {
	cliqFlip.Validate = { };

	cliqFlip.Validate.AddClassRules = function(className, validateRuleObj) {
		jQuery.validator.addClassRules(className, validateRuleObj);
	};

	return cliqFlip;
}(cliqFlip || { }));