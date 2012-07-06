//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var appController = {
        index: function() {
            cliqFlip.Mvc.App.contentRegion.show(new cliqFlip.Mvc.App.Views.LandingView());
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
            "": "index",
            "feed": "feed"
        },
        controller: appController
    });

    return cliqFlip;
} (CliqFlip));