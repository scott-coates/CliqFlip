var _bubbles = [];
var _mindMapSaveUrl;
var _canEdit = false;

function InitUser(canEdit) {
	_canEdit = canEdit;
}

function InitSocial(socialPageUrl) {
	if (!_canEdit) {
		$(".social-media-username-container")
			.css('cursor', 'pointer')
			.click(function () {
				window.location = socialPageUrl;
			});
	}
}

function InitMindMap(interests, saveUrl) {
	_mindMapSaveUrl = saveUrl;

	var r = Raphael("mindMap", 559, 300);

	if (interests && interests.length > 0) {
		var rowAxis = 80;
		var space = 100;
		var width = 130;
		for (var interest in interests) {
			if (interests[interest].Passion) {
			    _bubbles.push(r.cliqFlipCreateMindMapBubble(interests[interest].Passion,
					interests[interest].XAxis,
					interests[interest].YAxis,
					cliqFlip.Ui.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id,
					_canEdit,
					MindMapSave));
			}
			else {
				if ((parseFloat(interest) + 1) % 7 == 0) {
					rowAxis += 150;
					space = 100;
				}
				_bubbles.push(r.cliqFlipCreateMindMapBubble(2.5,
					space,
					rowAxis,
					cliqFlip.Ui.RandomHexColor(),
					interests[interest].Name,
					interests[interest].Id,
					_canEdit,
					MindMapSave));

				space += width;
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

	r.cliqFlipQuotationMarks(14);
	r.cliqFlipQuotationMarks(534);
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
		$("#websiteUrlText").eip(saveUrl, { select_text: true, form_type: "text" });
	}
}

function InitFacebook() {
	$("#facebook-connect").click(function (event) {
		var p = $(this).find("p");
		window.FB.login(function (response) {
			if (response.authResponse) {
				//send the user id to our app
				//TODO: don't hardcode /user/savefacebook - use Url.Action() so it takes routes 
				//and iis virtual apps into account
				$.post("/User/SaveFacebook", { fbid: response.authResponse.userID }, function () {
					p.html("Linked...");
				});
			}
			else {
				//TODO: tell the user something wen't wrong
			}
		});
	});
}