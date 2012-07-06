// @reference ~/Content/assets/js/marionette/backbone.marionette.js
// @reference ~/Content/app/config.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc = {};
    cliqFlip.Mvc.App = new Backbone.Marionette.Application();
    cliqFlip.Mvc.App.Models = {};
    cliqFlip.Mvc.App.Views = {};
    cliqFlip.Mvc.App.Collections = {};
    cliqFlip.Mvc.App.Routers = {};

    cliqFlip.Mvc.App.addRegions({
        contentRegion: "#main-content"
    });

    cliqFlip.Mvc.App.addInitializer(function() {
        cliqFlip.Mvc.App.appRouter = new cliqFlip.Mvc.App.Routers.AppRouter();
        Backbone.history.start({ pushState: true, root: "/home/bootstrap/" });
    });

    cliqFlip.Mvc.App.addInitializer(function() {
        var that = this;
        this.vent.on("feed:showList", function() {
            //dont directly invoke router function: http://lostechies.com/derickbailey/2011/08/28/dont-execute-a-backbone-js-route-handler-from-your-code/
            that.appRouter.navigate("feed");
            that.appRouter.controller.feed();
        });
    });

    cliqFlip.Mvc.App.addInitializer(function() {
        cliqFlip.Template.ApplyTemplateFix();
    });

    return cliqFlip;
} (CliqFlip || {}));