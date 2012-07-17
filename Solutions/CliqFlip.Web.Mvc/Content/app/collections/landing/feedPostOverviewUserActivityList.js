// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/landing/feedPostOverviewUserActivity.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Collections.FeedPostOverviewUserActivityList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.FeedPostOverviewUserActivity
        });

    return cliqFlip;
} (CliqFlip));