var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Views.FeedItemView = Backbone.Marionette.ItemView.extend({
        template: function() {
            var templates = ["feed-feedItem"];
            templates.push({ content: "media-Image" });
            return templates;
        },
        className: 'feed-item gray-rounded-border'
    });

    return cliqFlip;
} (CliqFlip));