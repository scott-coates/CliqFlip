// @reference PostOverviewUserAcitivtyView.js
var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyListView = Backbone.Marionette.CollectionView.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "comment:posted", this.postComment);
        },
        itemView: cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyView,
        postComment: function(comment) {
            var retVal = this.collection.create({
                    CommentText: comment.text,
                    PostId: comment.postId
                },
                {
                    wait: true /*wait for server confirmation before triggering 'add' event*/
                });
            if (retVal) {
                cliqFlip.App.Mvc.vent.trigger("comment:posted:success");
            }
        }
    });

    return cliqFlip;
}(CliqFlip));