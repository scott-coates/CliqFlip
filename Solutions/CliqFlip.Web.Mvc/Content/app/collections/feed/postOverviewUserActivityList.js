// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/post/postOverviewUserActivity.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Collections.PostOverviewUserActivityList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.PostOverviewUserActivity,
            url:'/api/post/'
        });

    return cliqFlip;
} (CliqFlip));