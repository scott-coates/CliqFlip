//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var landingController = {
        landing: function() {
            var landingLayout = new cliqFlip.App.Mvc.Layouts.LandingLayout();

            cliqFlip.App.Mvc.mainContentRegion.show(landingLayout);

            var userLandingSummaryModel = new cliqFlip.App.Mvc.Models.UserLandingSummary();
            userLandingSummaryModel.set(cliqFlip.App.UserData);
            var userLandingSummaryView = new cliqFlip.App.Mvc.Views.UserLandingSummaryView({ model: userLandingSummaryModel });
            landingLayout.leftColumnRegion.show(userLandingSummaryView);

            var feedList = new cliqFlip.App.Mvc.Collections.FeedList();

            feedList.fetch({
                success: function() {
                    landingLayout.contentAreaRegion.show(new cliqFlip.App.Mvc.Views.FeedListView({ collection: feedList }));
                }
            });
        },
        showPost: function(post) {
            var postOverview = new cliqFlip.App.Mvc.Models.FeedPostOverview({ id: post.get('PostId') });
            debugger;
            postOverview.fetch({
                success: function(parameters) {

                }
            });
            cliqFlip.App.Mvc.modalRegion.show(new cliqFlip.App.Mvc.Views.FeedPostOverviewView({ model: post }));
        }
    };

    cliqFlip.App.Mvc.Routers.LandingRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "": "landing"
        },
        controller: landingController
    });

    cliqFlip.App.Mvc.vent.bind("feedItem:selected", function(post) { landingController.showPost(post); });


    return cliqFlip;
} (CliqFlip));