//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
function InitMindMap() {
	var r = Raphael("mindMap", 940, 300);
	var connections = [];

	var updateConnections = function() {
		AdjustCollections(r, connections);
	};

	var i1 = r.cliqFlip.mindMapBubble(100, 100, "hsb(.25, 1, 1)", "Me\nfirst\nand\nthe\ngimmie\ngimmies", updateConnections);
	var i2 = r.cliqFlip.mindMapBubble(230, 100, "hsb(.09, 1, 1)", "some\nstuff", updateConnections);
	var i3 = r.cliqFlip.mindMapBubble(360, 100, "hsb(.15, 1, 1)", "some\nstuff", updateConnections);
	var i4 = r.cliqFlip.mindMapBubble(490, 100, "hsb(1, 1, 1)", "some\nstuff", updateConnections);
	var i5 = r.cliqFlip.mindMapBubble(620, 100, "hsb(.75, 1, 1)", "some\nstuff", updateConnections);
	var i6 = r.cliqFlip.mindMapBubble(750, 100, "hsb(.07, 1, 1)", "my\ninterest", updateConnections);

	connections.push(r.connection(i1.big, i2.big));
	connections.push(r.connection(i2.big, i3.big));
	connections.push(r.connection(i3.big, i4.big));
	connections.push(r.connection(i4.big, i5.big));
	connections.push(r.connection(i5.big, i6.big));
}

function AdjustCollections(r, connections) {
	for (var i = connections.length; i--;) {
		r.connection(connections[i]);
	}
}