//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
function InitMindMap(interests) {
	var r = Raphael("mindMap", 940, 300);

	var initialLayout = interests[0].Passion == null;
	if (initialLayout) {
		var rowAxis = 80;
		var space = 100;
		var width = 130;
		for (var interest in interests) {
			if((parseFloat(interest) + 1) % 7 == 0) {
				rowAxis += 150;
				space = 100;
			}

			r.cliqFlip.mindMapBubble(space, rowAxis, "hsb(.25, 1, 1)", interests[interest].Name);
			space += width;
		}
	}
}