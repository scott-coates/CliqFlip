var bubbles = [];

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

				bubbles.push(r.cliqFlip.mindMapBubble(2.5,
					space,
					rowAxis,
					cliqFlip.Utils.RandomHexColor(), 
					interests[interest].Name,
					interests[interest].Id));

				space += width;
			}
		}
	}
}

function InitMindMapSave() {
	$("#saveMindMap").click(function() {
		SaveMindMap();
	});
}

function SaveMindMap() {
	if (bubbles.length > 0) {
		var mindMapSave = [];
		for (var bubble in bubbles) {
			var bubbleObj = bubbles[bubble];
			mindMapSave.push({ userInterestId: bubbleObj.userInterestId });
		}
	}
}