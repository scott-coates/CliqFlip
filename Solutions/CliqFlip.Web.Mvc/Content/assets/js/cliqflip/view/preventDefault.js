//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var CliqFlip = (function(cliqFlip) {
	cliqFlip.View = cliqFlip.View || { };

	cliqFlip.View.PreventDefault = function(router) {
	    $(document).on('click', 'a:not([data-bypass])', function(evt) {

	        var href = $(this).attr('href');
	        var protocol = this.protocol + '//';

	        if(href.slice(protocol.length) !== protocol) {
	            evt.preventDefault();
	            router.navigate(href, true);
	        }
	    });
	};
	return cliqFlip;
} (CliqFlip || {}));