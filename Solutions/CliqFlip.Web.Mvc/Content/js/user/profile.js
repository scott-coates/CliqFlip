//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
function InitMindMap() {
	Raphael("mindMap", 940, 300)
		.cliqFlip.mindMapBubble(100,100, "hsb(.25, 1, 1)", "Me\nfirst\nand\nthe\ngimmie\ngimmies")
		.cliqFlip.mindMapBubble(230,100,"hsb(.09, 1, 1)", "some\nstuff")
		.cliqFlip.mindMapBubble(360, 100, "hsb(.15, 1, 1)", "some\nstuff")
		.cliqFlip.mindMapBubble(490, 100, "hsb(1, 1, 1)", "some\nstuff")
		.cliqFlip.mindMapBubble(620, 100, "hsb(.75, 1, 1)", "some\nstuff")
		.cliqFlip.mindMapBubble(750,100, "hsb(.07, 1, 1)", "my\ninterest");
}