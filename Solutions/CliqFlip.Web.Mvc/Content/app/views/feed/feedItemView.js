var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedItemView = Backbone.Marionette.ItemView.extend({
        template: function() {
            var templates = ["feed-feedItem"];
            templates.push({ content: "media-Image" });
            return templates;
        },
        className: 'feed-item gray-rounded-border',
        templateHelpers: cliqFlip.ViewHelpers,
        events: {
            "click .feed-image": "feedItemSelected" //TODO find a way to delegate from a parent (collection view) rather than each individual feed item view
        },
        feedItemSelected: function() {
            cliqFlip.App.Mvc.vent.trigger("feedItem:selected", this.model);
            //look into triggers: http://lostechies.com/derickbailey/2012/05/15/workflow-in-backbone-apps-triggering-view-events-from-dom-events/
        }
    });

    return cliqFlip;
} (CliqFlip));