var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout = Backbone.Marionette.Layout.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "comment:posted:success", this.clearComment); //bindTo will automatically be unbound on Close
        },
        template: "feed-feedPostOverview",
        className: "post-overview full-height",
        regions: {
            userActivityRegion: "#user-activity"
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