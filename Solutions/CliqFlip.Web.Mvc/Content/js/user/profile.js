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

				bubbles.push(r.cliqFlip.createMindMapBubble(2.5,
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

function InitMindMapSave(saveUrl) {
	$("#saveMindMap").click(function () {
		SaveMindMap(saveUrl);
	});
}

function SaveMindMap(saveUrl) {
	if (bubbles.length > 0) {
		var mindMapSave = [];
		for (var bubble in bubbles) {
			var bubbleObj = bubbles[bubble];
			mindMapSave.push({
				id: bubbleObj.userInterestId,
				xaxis: bubbleObj.big.attr('cx'),
				yaxis: bubbleObj.big.attr('cy'),
				passion: bubbleObj.GetPassion()
			});
		}
		
//		$.post(saveUrl, $.toJSON(mindMapSave), function (data) {
//			console.log('data ' + data);
//		}
		//		,"json");
//		console.log($.toJSON())
		   $.ajax(
        {
        	url: saveUrl,
            type: 'POST',
            data: $.toJSON(mindMapSave),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data, textStatus, jqXHR) {
            	console.log(textStatus);
            },
            error: function (objAJAXRequest, strError) {
            	console.log(strError);
            }
        }
     );   //ajax
	}
}