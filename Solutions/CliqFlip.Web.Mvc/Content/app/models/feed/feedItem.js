// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedItem = Backbone.Model.extend({
        like: function() {
            this.set('Like', true);
        }
    });

    return cliqFlip;
} (CliqFlip));