//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var appController = {
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
                    var feedViewList = new cliqFlip.App.Mvc.Views.FeedListView({ collection: feedList });
                    feedViewList.on("itemview:feedItem:selected", function(feedItemView) {
                        alert(feedItemView);
                    });
                    landingLayout.contentAreaRegion.show(feedViewList);
                }
            });
        }
    };

    cliqFlip.App.Mvc.Routers.AppRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "": "landing"
        },
        controller: appController
    });

    return cliqFlip;
} (CliqFlip));