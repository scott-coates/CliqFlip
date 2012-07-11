//http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
var CliqFlip = (function(cliqFlip) {
    cliqFlip.View = cliqFlip.View || {};

    cliqFlip.View.PreventLinkClickDefault = function(router) {
        $(document).on('click', 'a:not([data-bypass])', function(evt) {
            //sometimes we want to cancel a link click (even changing the url) (like if it's just a skeleton)
            if(!evt.isPropagationStopped()) {
                var href = $(this).attr('href');
                var protocol = this.protocol + '//';

                if(href.slice(protocol.length) !== protocol) {
                    evt.preventDefault();
                    router.navigate(href, true);
                }

                //TODO: analytics dely - look into https://github.com/jorkas/jquery-analyticseventtracking-plugin/commit/4f8e23c38bdbd25e6a48a32c8e712295ad7eb846
            }
        });
    };

    cliqFlip.View.HandleTracking = function() {
        window._gaq = window._gaq || [];
        $("[data-track]").analyticsEventTracking({
            delayed: false /*delayed because we don't want that lib to ever direct to href*/,
            category: function() {
                return $(this).data("category");
            },
            action: function() {
                return $(this).data("action");
            },
            label: function() {
                return $(this).data("label");
            }
        });

        $(document).on('click', '[data-category="skeleton"]', function(e) {
            e.stopPropagation(); //the buck stops here - no one else shoudl pay attention to this event
            e.preventDefault(); //don't change the address bar
            $("#comingSoonModal").modal();
        });
    };

    return cliqFlip;
} (CliqFlip || {}));