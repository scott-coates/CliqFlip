// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.CommentModel = Backbone.Model.extend({
        urlRoot: function() { return "/api/post/" + this.get('PostId') + "/comment/"; }
    });

    return cliqFlip;
} (CliqFlip));