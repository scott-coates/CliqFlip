// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.PostOverview = Backbone.Model.extend({
        urlRoot: '/api/post',
        like: function() {
            new cliqFlip.App.Mvc.Models.Like({ PostId: this.get('PostId') }).save();
            this.trigger('like');
        }
    });

    return cliqFlip;
} (CliqFlip));