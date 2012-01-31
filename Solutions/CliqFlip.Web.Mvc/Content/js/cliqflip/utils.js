//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var cliqFlip = (function (cliqFlip) {
	cliqFlip.my = {};
	var privateVariable = 1;

	function privateMethod() {
		// ...
	}

	cliqFlip.my.moduleProperty = 1;
	cliqFlip.my.moduleMethod = function () {
		alert('test');
	};



	return cliqFlip;
} (cliqFlip || {}));