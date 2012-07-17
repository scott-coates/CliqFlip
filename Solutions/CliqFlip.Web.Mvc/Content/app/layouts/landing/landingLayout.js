var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "landing-layout container-fluid gray-rounded-border",
        regions: {
            leftColumnRegion: "#left-column",
            contentAreaRegion: "#content-area"
        },
        events: {
            "click .user-interest-add-photo": "addPhoto"
        },
        addPhoto: function(parameters) {
            alert('add photo');
        }
    });

    return cliqFlip;
} (CliqFlip));