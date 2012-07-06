// @reference ~/Content/app/main.js

var CliqFlip = (function (cliqFlip) {

    cliqFlip.Mvc.App.Models.LandingPage = Backbone.Model.extend({
        urlRoot:"/api/user/landing"
    });

    return cliqFlip;
} (CliqFlip));