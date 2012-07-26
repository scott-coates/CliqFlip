// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedItem = Backbone.Model.extend({
        like: function() {
            new cliqFlip.App.Mvc.Models.Like({ PostId: this.get('PostId') }).save();
            this.set('IsLikedByUser', 'true');
        }
    });

    return cliqFlip;
} (CliqFlip));