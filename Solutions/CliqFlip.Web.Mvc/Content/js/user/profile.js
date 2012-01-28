//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
function InitMindMap() {
	var R = Raphael("mindMap", 940, 300)
		.cliqFlip.mindMapBubble("hsb(.25, 1, 1)", "some stuff")
		.cliqFlip.mindMapBubble("hsb(.09, 1, 1)", "some stuff")
		.cliqFlip.mindMapBubble("hsb(.15, 1, 1)", "some stuff")
		.cliqFlip.mindMapBubble("hsb(1, 1, 1)", "some stuff")
		.cliqFlip.mindMapBubble("hsb(.75, 1, 1)", "some stuff")
		.cliqFlip.mindMapBubble("hsb(.07, 1, 1)","my interest");
}