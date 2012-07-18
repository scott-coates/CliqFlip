// @reference ~/Content/assets/js/backbonePlugins/backbone.marionette.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App = {};
    cliqFlip.App.Mvc = new Backbone.Marionette.Application();
    cliqFlip.App.Mvc.Models = {};
    cliqFlip.App.Mvc.Views = {};
    cliqFlip.App.Mvc.Layouts = {};
    cliqFlip.App.Mvc.Collections = {};
    cliqFlip.App.Mvc.Routers = {};

    cliqFlip.App.Mvc.addRegions({
        headerRegion: "#header",
        mainContentRegion: "#main-content",
        modalRegion: function() { return new cliqFlip.View.ModalRegion({ el: "#main-modal" }); }
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

    cliqFlip.App.Mvc.addInitializer(function() {
        var headerModel = new cliqFlip.App.Mvc.Models.Header(cliqFlip.App.UserData);        
        var headerLayout = new cliqFlip.App.Mvc.Layouts.HeaderLayout({ model: headerModel });
        cliqFlip.App.Mvc.headerRegion.show(headerLayout);
        headerLayout.searchRegion.show(new cliqFlip.App.Mvc.Views.SearchView());
    });

    return cliqFlip;
} (CliqFlip || {}));