//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var appController = {
        landing: function() {
            var landingModel = new cliqFlip.Mvc.App.Models.LandingPage();
            landingModel.set(cliqFlip.Mvc.UserData);
            cliqFlip.Mvc.App.contentRegion.show(new cliqFlip.Mvc.App.Views.LandingView({ model: landingModel }));
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
            "": "landing",
            "feed": "feed"
        },
        controller: appController
    });

    return cliqFlip;
} (CliqFlip));