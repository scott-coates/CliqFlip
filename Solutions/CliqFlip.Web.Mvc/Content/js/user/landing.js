function InitFeed(limit, searchUrl) {
	var onSuccess = function (row, container) {
		container.append('<div>' + row.Description + '</div>');
	};

//	$('#interest-feed').jScroller(searchUrl, {
//		limit: limit,
//		onSuccessCallback: onSuccess
//	});
}