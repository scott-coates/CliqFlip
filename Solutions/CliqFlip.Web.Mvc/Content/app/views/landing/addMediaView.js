var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediaView = Backbone.Marionette.ItemView.extend({
        template: function(model) {
            var templates = ["landing-feedItem"];
            templates.push({ content: "media-Image" });
            return templates;
        },
        className: 'feed-item gray-rounded-border',
        templateHelpers: _.extend(cliqFlip.ViewHelpers, {
            hasRemainingComments: function() {
                return this.CommentCount > this.Comments.length;
            },
            commentCountDescription: function() {
                return this.CommentCount.toString() + " " + (this.CommentCount === 1 ? "Comment" : "Comments");
            }
        }),
        events: {
            "click .select-feed-item": "feedItemSelected", //TODO find a way to delegate from a parent (collection view) rather than each individual feed item view
            "click .like-interest-button": "likeInterest"
        },
        feedItemSelected: function() {
            cliqFlip.App.Mvc.vent.trigger("feedItem:selected", this.model);
            //look into triggers: http://lostechies.com/derickbailey/2012/05/15/workflow-in-backbone-apps-triggering-view-events-from-dom-events/
        },
        likeInterest: function() {
            this.model.like();
        }
    });

    return cliqFlip;
} (CliqFlip));