// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.FeedItemModel = Backbone.Model.extend({
        like: function() {
            new cliqFlip.App.Mvc.Models.LikeModel({ PostId: this.get('PostId') }).save();
            this.set('IsLikedByUser', 'true');
        }
    });

    return cliqFlip;
} (CliqFlip));