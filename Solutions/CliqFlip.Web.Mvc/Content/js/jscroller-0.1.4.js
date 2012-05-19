/**
 * jScroller Plugin 0.1.4
 *
 * Copyright (c) 2012  Renato Saito (renato.saito at live.com)
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 */

(function ($) {
	jQuery.fn.jScroller = function (store, parameters) {
		var currentlyLoading = false;
		var defaults = {
			limit: 10,
			onSuccessCallback: function (row, container) { },
			onErrorCallback: function (container, thrownError) { alert('An error occurred while trying to retrive data from store'); },
			onTimeoutCallback: function () { },
			timeout: 3 * 1000,
			loadingButtonText: 'Loading...',
			loadMoreButtonText: 'Load more',
			fullListText: 'There is no more items to show',
			ajaxType: 'POST'
		};
		var options = jQuery.extend(defaults, parameters);
		var list = jQuery(this),
            start = 0,
            total = 0;
		function loadItems(loadingByScroll) {
			if (!currentlyLoading) {
				currentlyLoading = true;
				preLoad();
				jQuery.ajax({
					url: store,
					type: options.ajaxType,
					data: {
						start: start,
						limit: options.limit
					},
					timeout: options.timeout,
					success: function (response) {
						list.find("#jscroll-loading").remove();
						if (response.Success === false) {
							options.onErrorCallback(list, response.Message);
							return;
						}
						total = response.total;
						start = start + options.limit;
						for (var i = 0; i < response.data.length; i++) {
							options.onSuccessCallback(response.data[i], list);
						}
						if (start >= total || response.data.length === 0) {
							list.append('<div id="jscroll-fulllist">' + options.fullListText + '</div>');
						}
						else if (loadingByScroll === false) {
							list.append('<div><button class="jscroll-loadmore">' + options.loadMoreButtonText + '</button></div>');
						}
						currentlyLoading = false;						
					},
					error: function (xhr, ajaxOptions, thrownError) {
						if (thrownError == 'timeout') {
							options.onTimeoutCallback();
						}
						else {
							options.onErrorCallback(list, thrownError);
						}
						currentlyLoading = false;												
					}
				});
			}
		}

		function startHandleLoadMoreButton() {
			list.on("click", ".jscroll-loadmore", function () {
				loadItems(false);
				jQuery(this).parent("div").remove();
			});
		}
		function preLoad() {
			if (list.find("#jscroll-loading").length === 0) {
				list.find(".jscroll-loadmore").parent("div").remove();
				list.append('<button id="jscroll-loading" disabled="disabled">' + options.loadingButtonText + '</button>');
			}
		}
		jQuery(window).scroll(function () {
			var window = jQuery(this);
			if (window.scrollTop() >= jQuery(document).height() - window.height() - 100) {
				if (start >= total) {
					return false;
				} else {
					loadItems(true);
				}
			}
			return false;
		});
		jQuery(document).ready(function () {
			startHandleLoadMoreButton();
			loadItems(false);
		});
	};
})(jQuery);