var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.PostOverviewLayout = Backbone.Marionette.Layout.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "comment:posted:success", this.clearComment); //bindTo will automatically be unbound on Close
            this.bindTo(this.model, "change", function() {
                cliqFlip.App.Mvc.vent.trigger("postItem:selected", this.model);
            });
        },
        template: function(model) {
            var templates = ["feed-postOverview"];

            var contentTemplate = "post-Image";
            if(model.VideoUrl) {
                contentTemplate = "post-Video";
            } else if(model.WebPageUrl) {
                contentTemplate = "post-WebPage";
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