//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var cliqFlip = (function(cliqFlip) {
	cliqFlip.Utils = { };

	cliqFlip.Utils.RandomHexColor = function() {
		//http://paulirish.com/2009/random-hex-color-code-snippets/
		return '#' + Math.floor(Math.random() * 16777215).toString(16);
	};
	return cliqFlip;
}(cliqFlip || { }));