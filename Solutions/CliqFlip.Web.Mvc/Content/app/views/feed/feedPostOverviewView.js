var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedPostOverviewView = Backbone.Marionette.ItemView.extend({
        template: "feed-feedPostOverview",
        className:"post-overview full-height"
    });

    return cliqFlip;
}(CliqFlip));