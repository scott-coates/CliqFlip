// @reference ~/Content/assets/js/marionette/backbone.marionette.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App = {};
    cliqFlip.App.Mvc = new Backbone.Marionette.Application();
    cliqFlip.App.Mvc.Models = {};
    cliqFlip.App.Mvc.Views = {};
    cliqFlip.App.Mvc.Layouts = {};
    cliqFlip.App.Mvc.Collections = {};
    cliqFlip.App.Mvc.Routers = {};

    cliqFlip.App.Mvc.addRegions({
        mainContentRegion: "#main-content",
        modal: function() { return new cliqFlip.View.ModalRegion({ el: "#main-modal" }); }
    });

    cliqFlip.App.Mvc.addInitializer(function() {
        cliqFlip.App.Mvc.landingRouter = new cliqFlip.App.Mvc.Routers.LandingRouter();
        cliqFlip.View.HandleTracking();
        cliqFlip.View.PreventLinkClickDefault();
        Backbone.history.start({ pushState: true, root: "/home/bootstrap/" });
    });

    cliqFlip.App.Mvc.addInitializer(function() {
        cliqFlip.Template.ApplyTemplateFix();
    });

    return cliqFlip;
} (CliqFlip || {}));