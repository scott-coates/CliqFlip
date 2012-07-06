// @reference ~/Content/assets/js/marionette/backbone.marionette.js
// @reference ~/Content/app/config.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc = {};
    cliqFlip.Mvc.App = new Backbone.Marionette.Application();
    cliqFlip.Mvc.App.Models = {};
    cliqFlip.Mvc.App.Views = {};
    cliqFlip.Mvc.App.Layouts = {};
    cliqFlip.Mvc.App.Collections = {};
    cliqFlip.Mvc.App.Routers = {};

    cliqFlip.Mvc.App.addRegions({
        mainContentRegion: "#main-content"
    });

    cliqFlip.Mvc.App.addInitializer(function() {
        cliqFlip.Mvc.App.appRouter = new cliqFlip.Mvc.App.Routers.AppRouter();
        Backbone.history.start({ pushState: true, root: "/home/bootstrap/" });
    });

    cliqFlip.Mvc.App.addInitializer(function() {
        cliqFlip.Template.ApplyTemplateFix();
    });

    return cliqFlip;
} (CliqFlip || {}));