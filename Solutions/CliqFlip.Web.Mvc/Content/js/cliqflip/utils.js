//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var cliqFlip = (function(cliqFlip) {
	cliqFlip.Utils = { };

	cliqFlip.Utils.RandomHexColor = function() {
//		http: //css-tricks.com/snippets/javascript/random-hex-color/#comment-83815
		var x = Math.round(0xffffff * Math.random()).toString(16);
		var y = (6 - x.length);
		var z = '000000';
		var z1 = z.substring(0, y);
		var color = '#' + z1 + x;
		return color;
	};
	return cliqFlip;
}(cliqFlip || { }));