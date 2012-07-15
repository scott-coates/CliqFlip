// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Models.FeedPostOverviewUserActivity = Backbone.Model.extend({
        urlRoot: '/search/feedpostoverviewuseractivity',
        validate: function(attrs) {
            if(attrs.CommentText.lenght <= 0) {
                return 'comment is required';
            }
        }
    });

    return cliqFlip;
} (CliqFlip));