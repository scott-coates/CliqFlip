// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.PostOverview = Backbone.Model.extend({
        urlRoot: '/api/post',
        like: function() {
            var that = this;
            new cliqFlip.App.Mvc.Models.Like().save('PostId', this.get('PostId'), {
                success: function() {
                    that.set('IsLikedByUser', 'true');
                }
            });
        }
    });

    return cliqFlip;
} (CliqFlip));