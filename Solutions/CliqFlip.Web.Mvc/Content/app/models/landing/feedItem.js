// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedItem = Backbone.Model.extend({
        urlRoot: '/search/feeditem',
        like: function() {
            this.save('IsLikedByUser', true);
        }
    });

    return cliqFlip;
} (CliqFlip));