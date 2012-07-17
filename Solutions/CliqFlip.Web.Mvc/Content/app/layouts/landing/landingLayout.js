var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "landing-layout container-fluid gray-rounded-border",
        regions: {
            leftColumnRegion: "#left-column",
            contentAreaRegion: "#content-area"
        },
        events: {
            "click a[class*='user-interest-add']": "addMedium"
        },
        addMedium: function(e) {
            var addMediumModel = new cliqFlip.App.Mvc.Models.AddMedium(cliqFlip.App.UserData);
            addMediumModel.set('MediumType', $(e.target).data('mediumType'));
            var mediumView = new cliqFlip.App.Mvc.Views.AddMediumView({ model: addMediumModel });
            cliqFlip.App.Mvc.modalRegion.show(mediumView);
            mediumView.on("medium:added", function() {
                cliqFlip.App.Mvc.modalRegion.hideModal();
            });

        }
    });

    return cliqFlip;
} (CliqFlip));