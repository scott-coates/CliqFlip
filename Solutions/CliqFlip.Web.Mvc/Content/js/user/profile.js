var _bubbles = [];
var _mindMapSaveUrl;
var _canEdit = false;

function InitUser(canEdit) {
	_canEdit = canEdit;
}

function InitMindMap(interests, saveUrl) {
	_mindMapSaveUrl = saveUrl;

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

				_bubbles.push(r.cliqFlip.createMindMapBubble(2.5,
					space,
					rowAxis,
					cliqFlip.Ui.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id,
					_canEdit,
					MindMapSave));

				space += width;

			}
		} else {
			for (interest in interests) {
				_bubbles.push(r.cliqFlip.createMindMapBubble(interests[interest].Passion,
					interests[interest].XAxis,
					interests[interest].YAxis,
					cliqFlip.Ui.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id,
					_canEdit,
					MindMapSave));
			}
		}
	}
}

function MindMapSave(mindMapObj) {
	var interest = {
		id: mindMapObj.userInterestId,
		xaxis: mindMapObj.big.attr('cx'),
		yaxis: mindMapObj.big.attr('cy'),
		passion: mindMapObj.GetPassion()
	};

	$.post(_mindMapSaveUrl, interest, function (data) {
		var jqObj = $("#saveMindMapText");
		cliqFlip.Ui.Blink(jqObj);
	}, "json");
}

function InitHeadline(saveUrl) {
	var r = Raphael("profileHeadline", 559, 50);

	r.cliqFlip.quotationMarks(14);
	r.cliqFlip.quotationMarks(534);
	if (_canEdit) {
		var textTemplate = '<input type="text" id="edit-#{id}" class="#{editfield_class}" value="#{value}" maxlength="50" /> <br />';
		$("#headlineText").eip(saveUrl, { select_text: false, text_form: textTemplate });
	}
}

function InitBio(saveUrl) {
	if (_canEdit) {
		$("#bioText").eip(saveUrl, { select_text: false, form_type: "textarea" });
	}
}

function InitTwitterUsername(saveUrl) {
	if (_canEdit) {
		$("#twitterUsernameText").eip(saveUrl, { select_text: true, form_type: "text" });
	}
}

function InitYouTubeUsername(saveUrl) {
	if (_canEdit) {
		$("#youTubeUsernameText").eip(saveUrl, { select_text: true, form_type: "text" });
	}
}

function InitWebsiteUrl(saveUrl) {
	if (_canEdit) {
		$("#websiteText").eip(saveUrl, { select_text: true, form_type: "text" });
	}
}