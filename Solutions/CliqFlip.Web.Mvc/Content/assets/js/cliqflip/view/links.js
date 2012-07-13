var CliqFlip = (function(cliqFlip) {
    cliqFlip.View = cliqFlip.View || {};

    cliqFlip.View.PreventLinkClickDefault = function() {
        $(document).on('click', 'a:not([data-bypass])', function(evt) {
            //sometimes we want to cancel a link click (even changing the url) (like if it's just a skeleton)
            if(!evt.isPropagationStopped()) {
                var href = $(this).attr('href');
                var protocol = this.protocol + '//';

                if(href.slice(protocol.length) !== protocol) {
                    evt.preventDefault();
                    Backbone.history.navigate(href, false); //safe to call just this obj http://stackoverflow.com/questions/11282145/multiple-routers-in-a-backbone-maybe-marionette-app
                    //It is recommended that you divide your controller objects into smaller pieces of related functionality and have multiple routers / controllers, instead of just one giant router and controller. https://github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md
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