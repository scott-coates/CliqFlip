var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout = Backbone.Marionette.Layout.extend({
        template: "feed-feedPostOverview",
        className: "post-overview full-height",
        regions: {
            userActivityRegion: "#user-activity"
        },
        events: {
            "click #post-overview-comment-button": "addComment"
        },
        addComment: function(parameters) {
            alert(parameters);
        }
    });

    return cliqFlip;
}(CliqFlip));