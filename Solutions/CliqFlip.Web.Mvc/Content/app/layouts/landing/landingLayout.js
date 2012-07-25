var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Layouts.LandingLayout = Backbone.Marionette.Layout.extend({
        template: "landing-layout",
        className: "landing-layout container-fluid gray-rounded-border",
        regions: {
            userRegion: "#user-column",
            contentAreaRegion: "#content-area"
        },
        events: {
            "click a[class*='user-interest-add']": "addPost"
        },
        addPost: function(e) {
            var post = new cliqFlip.App.Mvc.Models.Post(cliqFlip.App.UserData);
            post.set('PostType', $(e.target).data('postType'));
            var postView = new cliqFlip.App.Mvc.Views.AddPostView({ model: post });
            cliqFlip.App.Mvc.modalRegion.show(postView);
            postView.on("post:added", function() {
                cliqFlip.App.Mvc.modalRegion.hideModal();
            });
        }
    });

    return cliqFlip;
} (CliqFlip));