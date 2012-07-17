var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.HeaderLayout = Backbone.Marionette.Layout.extend({
        template: "header-layout",
        className: "navbar navbar-fixed-top",
        regions: {
            searchRegion: "#search-container"
        }
    });

    return cliqFlip;
}(CliqFlip));