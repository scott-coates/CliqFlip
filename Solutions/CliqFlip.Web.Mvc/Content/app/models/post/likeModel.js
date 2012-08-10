// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.LikeModel = Backbone.Model.extend({
        urlRoot: function() { return "/api/post/" + this.get('PostId') + "/like/"; }
    });

    return cliqFlip;
} (CliqFlip));