var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout = Backbone.Marionette.Layout.extend({
        initialize: function() {
            cliqFlip.App.Mvc.vent.bind("comment:posted:success", this.clearComment);
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
            alert('cleared out');
            commentElem.val('');
        },
        onClose: function() {
            cliqFlip.App.Mvc.vent.unbind("comment:posted:success", this.clearComment);
        }
    });

    return cliqFlip;
} (CliqFlip));