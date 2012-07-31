var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedItemView = Backbone.Marionette.ItemView.extend({
        initialize: function() {
            this.bindTo(this.model, "change", function() {
                this.render();
            });
        },
        template: function(model) {
            var templates;

            if(model.FeedItemType === 'User') {
                templates = ["feed-userFeedItem"];
            }
            else {
                templates = ["feed-postFeedItem"];
                templates.push({ content: "media-Image" });
            }

            return templates;
        },
        className: 'feed-item gray-rounded-border invisible',
        templateHelpers: _.extend(cliqFlip.ViewHelpers, {
            hasRemainingComments: function() {
                return this.CommentCount > this.Comments.length;
            },
            commentCountDescription: function() {
                return this.CommentCount.toString() + " " + (this.CommentCount === 1 ? "Comment" : "Comments");
            },
            interestCountDescription: function(interestCount, interestType) {
                return interestCount.toString() + " " + interestType + " " + (interestCount === 1 ? "Interest" : "Interests");
            },
            commonInterestCountDescription: function() {
                return this.interestCountDescription(this.CommonInterestCount, "Common");
            },
            relatedInterestCountDescription: function() {
                return this.interestCountDescription(this.RelatedInterestCount, "Related");
            }
        }),
        events: {
            "click .select-post-item": "postItemSelected", //TODO find a way to delegate from a parent (collection view) rather than each individual feed item view
            "click .like-interest-button": "likeInterest",
            "click .select-user-button": "userItemSelected"
        },
        postItemSelected: function() {
            cliqFlip.App.Mvc.vent.trigger("postItem:selected", this.model);
            //TODO look into triggers: http://lostechies.com/derickbailey/2012/05/15/workflow-in-backbone-apps-triggering-view-events-from-dom-events/
        },
        likeInterest: function() {
            this.model.like();
        },
        userItemSelected: function() {
            cliqFlip.App.Mvc.vent.trigger("userItem:selected", this.model);
        }
    });

    return cliqFlip;
} (CliqFlip));