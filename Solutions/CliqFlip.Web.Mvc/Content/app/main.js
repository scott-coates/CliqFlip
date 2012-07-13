// @reference ~/Content/assets/js/marionette/backbone.marionette.js

var CliqFlip = (function (cliqFlip) {

    cliqFlip.App = {};
    cliqFlip.App.Mvc = new Backbone.Marionette.Application();
    cliqFlip.App.Mvc.Models = {};
    cliqFlip.App.Mvc.Views = {};
    cliqFlip.App.Mvc.Layouts = {};
    cliqFlip.App.Mvc.Collections = {};
    cliqFlip.App.Mvc.Routers = {};

    cliqFlip.App.Mvc.addRegions({
        mainContentRegion: "#main-content",
        modalRegion: function() { return new cliqFlip.View.ModalRegion({ el: "#main-modal" }); }
    });

    cliqFlip.App.Mvc.addInitializer(function () {
        cliqFlip.App.Mvc.landingRouter = new cliqFlip.App.Mvc.Routers.LandingRouter();
        cliqFlip.View.HandleTracking();
        cliqFlip.View.PreventLinkClickDefault();
        Backbone.history.start({ pushState: true, root: "/home/bootstrap/" });
    });

    cliqFlip.App.Mvc.addInitializer(function () {
        cliqFlip.Template.ApplyTemplateFix();
    });

    cliqFlip.App.Mvc.addInitializer(function () {
        //var headerLayout = new cliqFlip.App.Mvc.Layouts.HeaderLayout();

        var search = new cliqFlip.App.Mvc.Views.SearchView();

        //headerLayout.searchRegion.show();
        //cliqFlip.App.Mvc.header.show(search);

    });



    return cliqFlip;
} (CliqFlip || {}));