var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "container gray-rounded-border",
        regions: {
            leftColumnRegion: "#left-column",
            contentAreaRegion: "#content-area"
        }
    });

    return cliqFlip;
}(CliqFlip));