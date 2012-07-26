// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Models.PostOverviewUserActivity = Backbone.Model.extend({
        urlRoot: '/api/comment',
        validate: function(attrs) {
            if(attrs.CommentText.length <= 0) {
                return 'comment is required';
            }
        }
    });

    return cliqFlip;
} (CliqFlip));