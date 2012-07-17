// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedPostOverview = Backbone.Model.extend({
        urlRoot: '/search/feedpostoverview'
    });

    return cliqFlip;
}(CliqFlip));