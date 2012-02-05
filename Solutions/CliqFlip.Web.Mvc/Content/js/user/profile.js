var bubbles = [];

function InitMindMap(interests) {

	var r = Raphael("mindMap", 940, 300);

	if (interests && interests.length > 0) {


		var initialLayout = !interests[0].Passion;
		if (initialLayout) {
			var rowAxis = 80;
			var space = 100;
			var width = 130;
			for (var interest in interests) {
				if ((parseFloat(interest) + 1) % 7 == 0) {
					rowAxis += 150;
					space = 100;
				}

				bubbles.push(r.cliqFlip.createMindMapBubble(2.5,
					space,
					rowAxis,
					cliqFlip.Utils.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id));

				space += width;
			}
		} else {
			for (interest in interests) {
				bubbles.push(r.cliqFlip.createMindMapBubble(interests[interest].Passion,
					interests[interest].XAxis,
					interests[interest].YAxis,
					cliqFlip.Utils.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id));
			}
		}
	}
}

function InitMindMapSave(saveUrl, userId) {
	$("#saveMindMap").click(function () {
		SaveMindMap(saveUrl, userId);
	});
}

function SaveMindMap(saveUrl, userId) {
	if (bubbles.length > 0) {
		var mindMapData = { userId: userId };

		var mindMapInterests = [];

		mindMapData.Interests = mindMapInterests;

		for (var bubble in bubbles) {
			var bubbleObj = bubbles[bubble];
			mindMapInterests.push({
				id: bubbleObj.userInterestId,
				xaxis: bubbleObj.big.attr('cx'),
				yaxis: bubbleObj.big.attr('cy'),
				passion: bubbleObj.GetPassion()
			});
		}

		//http://stackoverflow.com/questions/2845459/jquery-how-to-make-post-use-contenttype-application-json		
		var mindMapDataJson = $.toJSON(mindMapData);
		$.ajax(
			{
				url: saveUrl,
				type: 'POST',
				data: mindMapDataJson,
				contentType: 'application/json; charset=utf-8',
				dataType: 'json',
				success: function (data, textStatus, jqXHR) {
					//TODO:use console.debug 
					console.log(textStatus);
				},
				error: function (objAJAXRequest, strError) {
					console.log(strError);
				}
			}
		);
	}
}

function InitHeadline() {
	var r = Raphael("profileHeadline", 940, 50);
	r.path("M14.505,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.128,0,2.07,0.411,2.826,1.229c0.756,0.82,1.134,1.832,1.134,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.259,0.443-4.327,1.332-6.203c0.888-1.875,2.243-3.57,4.067-5.085c1.824-1.514,2.988-2.272,3.492-2.272c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846L14.505,5.873zM27.465,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.104,0,2.04,0.411,2.808,1.229c0.769,0.82,1.152,1.832,1.152,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.284,0.449-4.369,1.35-6.256c0.9-1.887,2.256-3.577,4.068-5.067c1.812-1.49,2.97-2.236,3.474-2.236c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846L27.465,5.873z")
		.attr({ fill: "#000", stroke: "none" });
	r.path("M919.505,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.128,0,2.07,0.411,2.826,1.229c0.756,0.82,1.134,1.832,1.134,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.259,0.443-4.327,1.332-6.203c0.888-1.875,2.243-3.57,4.067-5.085c1.824-1.514,2.988-2.272,3.492-2.272c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846zM932.465,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.104,0,2.04,0.411,2.808,1.229c0.769,0.82,1.152,1.832,1.152,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.284,0.449-4.369,1.35-6.256c0.9-1.887,2.256-3.577,4.068-5.067c1.812-1.49,2.97-2.236,3.474-2.236c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846z")
		.attr({ fill: "#000", stroke: "none" });
}