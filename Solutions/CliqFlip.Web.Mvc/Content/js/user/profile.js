function InitMindMap(interests) {

	var r = Raphael("mindMap", 940, 300);

	if (interests && interests.length > 0) {


		var initialLayout = interests[0].Passion == null;
		if (initialLayout) {
			var rowAxis = 80;
			var space = 100;
			var width = 130;
			for (var interest in interests) {
				if ((parseFloat(interest) + 1) % 7 == 0) {
					rowAxis += 150;
					space = 100;
				}

				r.cliqFlip.mindMapBubble(space, rowAxis, cliqFlip.Utils.RandomHexColor(), interests[interest].Name);
				space += width;
			}
		}
	}
}

function InitMindMapSave() {
	
}