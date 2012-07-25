// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedItem = Backbone.Model.extend({
        like: function() {
            this.save('IsLikedByUser', true, { url: '/api/like' });
        }
    });

    return cliqFlip;
} (CliqFlip));