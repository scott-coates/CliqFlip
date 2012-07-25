// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.PostOverview = Backbone.Model.extend({
        urlRoot: '/api/post',
        like: function() {
            this.save('IsLikedByUser', true, { url: '/api/like' });
        }
    });

    return cliqFlip;
} (CliqFlip));