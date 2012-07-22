// @reference PostOverviewUserAcitivtyView.js
var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyListView = Backbone.Marionette.CollectionView.extend({
        initialize: function() {
            cliqFlip.App.Mvc.vent.bind("comment:posted", _.bind(this.postComment, this));
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