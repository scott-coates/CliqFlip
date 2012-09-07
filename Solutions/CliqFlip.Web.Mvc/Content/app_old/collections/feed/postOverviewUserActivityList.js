// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/post/postOverviewUserActivityModel.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Collections.PostOverviewUserActivityList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.PostOverviewUserActivityModel,
            url:'/api/post/'
        });

    return cliqFlip;
} (CliqFlip));