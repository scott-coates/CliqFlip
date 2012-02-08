var bubbles = [];

function InitMindMap(interests) {

	var r = Raphael("mindMap", 559, 300);

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
					interests[interest].Id,
					MindMapBlinkSave));

				space += width;
			}
		} else {
			for (interest in interests) {
				bubbles.push(r.cliqFlip.createMindMapBubble(interests[interest].Passion,
					interests[interest].XAxis,
					interests[interest].YAxis,
					cliqFlip.Utils.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id,
					MindMapBlinkSave));
			}
		}
	}
}

function MindMapBlinkSave() {
	var jqObj = $("#saveMindMapText");
	cliqFlip.Utils.Blink(jqObj);
}

function InitMindMapSave(saveUrl, userId) {
	$("#saveMindMap").click(function() {
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
				success: function(data, textStatus, jqXHR) {
					//TODO:use console.debug 
					console.log(textStatus);
				},
				error: function(objAJAXRequest, strError) {
					console.log(strError);
				}
			}
		);
	}
}

function InitHeadline(saveUrl) {
	var r = Raphael("profileHeadline", 559, 50);

	r.cliqFlip.quotationMarks(14);
	r.cliqFlip.quotationMarks(534);

	var textTemplate = '<input type="text" id="edit-#{id}" class="#{editfield_class}" value="#{value}" maxlength="50" /> <br />';
	$("#headlineText").eip(saveUrl, { select_text: false, text_form: textTemplate });
}

function InitBio(saveUrl) {
	$("#bioText").eip(saveUrl, { select_text: false, form_type: "textarea" });
}