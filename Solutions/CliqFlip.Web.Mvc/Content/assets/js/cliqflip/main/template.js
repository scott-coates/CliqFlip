//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var CliqFlip = (function(cliqFlip) {
	cliqFlip.Template = { };

	cliqFlip.Template.ApplyTemplateFix = function () {
	    //so Cassette assigns the compiled output to the 'render' property
	    //of the hogan template object instead of the 'r' property like it should.

	    //this mixup causes partial templates not to render because
	    //partial templates are rendered using the internal render function
	    //which calls 'r' to render the template

	    //so to fix it set the value of 'r' to the value of 'render'
	    //a fix has been submitted
	    
	    //fix all the hogan templates
	    _.each(window.JST, function (template) {
	        template.r = template.render;
	        template.render = HoganTemplate.prototype.render;
	    });
	};
	return cliqFlip;
} (CliqFlip || {}));