var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "container gray-rounded-border",
        regions: {
            leftColumn: "#left-column",
            contentArea: "#content-area"
        }
    });

    return cliqFlip;
}(CliqFlip));