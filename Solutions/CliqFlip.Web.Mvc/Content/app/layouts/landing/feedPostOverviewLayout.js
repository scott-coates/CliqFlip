var CliqFlip = (function (cliqFlip) {

    cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout = Backbone.Marionette.Layout.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "comment:posted:success", this.clearComment); //bindTo will automatically be unbound on Close
        },
   template: function (model) {
            var templates = ["feed-feedPostOverview"];

            var contentTemplate = "media-Image"
            if (model.VideoUrl) {
                contentTemplate = "media-Video";
            } else if (model.WebPageUrl) {
                contentTemplate = "media-WebPage";
            }

            templates.push({ content: contentTemplate });
            return templates;
        },

        className: "post-overview full-height",
        regions: {
            userActivityRegion: "#user-activity",
            contentRegion: "#post-content-region"
        },
        events: {
            "click #post-overview-comment-button": "addComment"
        },
        addComment: function() {
            var commentElem = $("#post-overview-comment-content", this.$el);
            var text = commentElem.val();
            cliqFlip.App.Mvc.vent.trigger("comment:posted", { text: text, postId: this.model.id });
        },
        clearComment: function() {
            var commentElem = $("#post-overview-comment-content", this.$el);
            commentElem.val('');
        }
    });

    return cliqFlip;
} (CliqFlip));