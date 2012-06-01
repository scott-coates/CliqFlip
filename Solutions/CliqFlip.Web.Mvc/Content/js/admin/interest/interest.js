var _addInterestDialog;

function InitInterest(relatedInterests) {
	_addInterestDialog = $("#add-interest").dialog({
		autoOpen: false,
		modal: true,
		width: 600,
		height: 600
	});

	$("#add-interest-button").click(function () {
		_addInterestDialog.dialog("open");
	});

	var containerName = "interest-relationship";
	var container = $("#" + containerName);
	var width = container.width() - 20;
	var height = container.height() - 20;
	var g = new Graph();

	for (var node in relatedInterests.RelatedInterests) {
		debugger;
		var relatedInterest = relatedInterests.RelatedInterests[node];
		var fillSize = (relatedInterest.Weight * 10).toString();
		g.addEdge(relatedInterests.MainInterest.Name, relatedInterest.Interest.Name, { stroke: "#bfa", fill: "#56f|" + fillSize, label: relatedInterest.Weight });
	}

	var layouter = new Graph.Layout.Spring(g);
	layouter.layout();

	var renderer = new Graph.Renderer.Raphael(containerName, g, width, height);
	renderer.draw();
}