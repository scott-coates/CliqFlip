//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var appController = {
        landing: function() {
            var landingLayout = new cliqFlip.Mvc.App.Layouts.LandingLayout();
           
            cliqFlip.Mvc.App.mainContentRegion.show(landingLayout);

            var userLandingSummaryModel = new cliqFlip.Mvc.App.Models.UserLandingSummary();
            userLandingSummaryModel.set(cliqFlip.Mvc.UserData);
            var userLandingSummaryView = new cliqFlip.Mvc.App.Views.UserLandingSummaryView({ model: userLandingSummaryModel });
            landingLayout.leftColumn.show(userLandingSummaryView);
        },
        feed: function() {
            var feedList = new cliqFlip.Mvc.App.Collections.FeedList();

            feedList.fetch({
                success: function() {
                    cliqFlip.Mvc.App.contentRegion.show(new cliqFlip.Mvc.App.Views.FeedListView({ collection: feedList }));
                }
            });
        }
    };

    cliqFlip.Mvc.App.Routers.AppRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "": "landing"
        },
        controller: appController
    });

    return cliqFlip;
} (CliqFlip));