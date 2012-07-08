var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedItemView = Backbone.Marionette.ItemView.extend({
        template: function() {
            var templates = ["feed-feedItem"];
            templates.push({ content: "media-Image" });
            return templates;
        },
        className: 'feed-item gray-rounded-border',
        templateHelpers: cliqFlip.App.Mvc.ViewHelpers
    });

    return cliqFlip;
} (CliqFlip));