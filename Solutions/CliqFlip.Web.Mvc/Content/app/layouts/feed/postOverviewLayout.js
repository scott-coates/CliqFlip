var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.PostOverviewLayout = Backbone.Marionette.Layout.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "comment:posted:success", this.clearComment); //bindTo will automatically be unbound on Close
            this.bindTo(this.model, "like", function() {
                cliqFlip.App.Mvc.vent.trigger("feedItem:selected", this.model);
            });
        },
        template: function(model) {
            var templates = ["feed-postOverview"];

            var contentTemplate = "media-Image";
            if(model.VideoUrl) {
                contentTemplate = "media-Video";
            } else if(model.WebPageUrl) {
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
            "click #post-overview-comment-button": "addComment",
            "click .like-interest-button": "likeInterest"
        },
        addComment: function() {
            var commentElem = $("#post-overview-comment-content", this.$el);
            var text = commentElem.val();
            cliqFlip.App.Mvc.vent.trigger("comment:posted", { text: text, postId: this.model.id });
        },
        clearComment: function() {
            var commentElem = $("#post-overview-comment-content", this.$el);
            commentElem.val('');
        },
        likeInterest: function() {
            this.model.like();
        }
    });

    return cliqFlip;
} (CliqFlip));