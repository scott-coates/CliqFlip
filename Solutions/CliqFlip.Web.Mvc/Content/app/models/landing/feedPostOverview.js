// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedPostOverview = Backbone.Model.extend({
        urlRoot: '/search/feedpostoverview',
        like: function() {
            this.save('IsLikedByUser', true, { url: '/search/feeditem' });
        }
    });

    return cliqFlip;
}(CliqFlip));