var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout = Backbone.Marionette.Layout.extend({
        template: "feed-feedPostOverview",
        className: "post-overview full-height",
        regions: {
            userActivityRegion: "#user-activity"
        },
        events: {
            "click #post-overview-comment-button": "addComment"
        },
        addComment: function(parameters) {
            var commentElem = $("#post-overview-comment-content", this.$el);
            var text = commentElem.val();
            commentElem.val('');
            this.userActivityRegion.currentView.collection.create({
                CommentText: text,
                PostId: this.model.id
            },
                { wait: true /*don't render the view till we get all the data back from the server*/ });
        }
    });

    return cliqFlip;
} (CliqFlip));