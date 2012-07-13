var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedPostOverviewView = Backbone.Marionette.ItemView.extend({
        template: "feed-feedPostOverview",
        className: "post-overview full-height",
        events: {
            "click .post-overview-comment-button": "addComment"
        },
        addComment: function(parameters) {
            alert(parameters);
        }
    });

    return cliqFlip;
} (CliqFlip));