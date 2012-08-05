// @reference ~/Content/assets/scripts/backbonePlugins/backbone.marionette.js

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
        landingRegion: Backbone.Marionette.Region.extend({
            el: "#landing-content"
        }),
        userRegion: Backbone.Marionette.Region.extend({
            el: "#user-content",
            onShow: function() {
                cliqFlip.App.Mvc.landingRegion.$el.hide();
            }
        }),
        modalRegion: function() { return new cliqFlip.View.ModalRegion({ el: "#main-modal" }); }
    });

    cliqFlip.App.Mvc.addInitializer(function() {
        //header layout
        var headerModel = new cliqFlip.App.Mvc.Models.Header(cliqFlip.App.UserData);
        var headerLayout = new cliqFlip.App.Mvc.Layouts.HeaderLayout({ model: headerModel });
        cliqFlip.App.Mvc.headerRegion.show(headerLayout);
        headerLayout.searchRegion.show(new cliqFlip.App.Mvc.Views.SearchView());
    });

    cliqFlip.App.Mvc.addInitializer(function() {
        //routers
        cliqFlip.App.Mvc.landingRouter = new cliqFlip.App.Mvc.Routers.LandingRouter();
        cliqFlip.App.Mvc.searchRouter = new cliqFlip.App.Mvc.Routers.SearchRouter();
        cliqFlip.App.Mvc.userRouter = new cliqFlip.App.Mvc.Routers.UserRouter();
        Backbone.history.start({ pushState: true, root: "/home/bootstrap/" });
    });

    cliqFlip.App.Mvc.addInitializer(function() {
        //misc (fixes, global handling, etc)
        cliqFlip.View.HandleTracking();
        cliqFlip.View.PreventLinkClickDefault();
    });

    return cliqFlip;
} (CliqFlip || {}));