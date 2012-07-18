var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "landing-layout container-fluid gray-rounded-border",
        regions: {
            userRegion: "#user-column",
            contentAreaRegion: "#content-area"
        },
        events: {
            "click a[class*='user-interest-add']": "addMedium"
        },
        addMedium: function(e) {
            var addMediumModel = new cliqFlip.App.Mvc.Models.AddPost(cliqFlip.App.UserData);
            addMediumModel.set('PostType', $(e.target).data('postType'));
            var mediumView = new cliqFlip.App.Mvc.Views.AddPostView({ model: addMediumModel });
            cliqFlip.App.Mvc.modalRegion.show(mediumView);
            mediumView.on("post:added", function() {
                cliqFlip.App.Mvc.modalRegion.hideModal();
            });

        }
    });

    return cliqFlip;
} (CliqFlip));