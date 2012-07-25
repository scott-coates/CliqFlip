// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/feed/postOverviewUserActivity.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Collections.PostOverviewUserActivityList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.PostOverviewUserActivity
        });

    return cliqFlip;
} (CliqFlip));