// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Models.FeedPostOverviewUserActivity = Backbone.Model.extend({
        urlRoot: '/search/feedpostoverviewuseractivity'
    });

    return cliqFlip;
} (CliqFlip));