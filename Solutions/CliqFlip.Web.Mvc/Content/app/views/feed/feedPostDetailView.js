var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedPostDetailView = Backbone.Marionette.ItemView.extend({
        template: "feed-feedPostDetail",
        className:"post-details"
    });

    return cliqFlip;
}(CliqFlip));