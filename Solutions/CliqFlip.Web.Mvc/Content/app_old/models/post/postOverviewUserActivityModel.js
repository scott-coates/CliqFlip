// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Models.PostOverviewUserActivityModel = Backbone.Model.extend({
        urlRoot: function() { return "/api/post/" + this.get('PostId') + "/comment/"; },
        validate: function(attrs) {
            if(attrs.CommentText.length <= 0) {
                return 'comment is required';
            }
            return false;
        }
    });

    return cliqFlip;
} (CliqFlip));