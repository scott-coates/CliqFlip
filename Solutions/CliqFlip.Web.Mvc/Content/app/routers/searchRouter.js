//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var searchController = {
        doSearch: function(search) {
            var feedList = new cliqFlip.App.Mvc.Collections.FeedList();

            feedList.fetch({
                data: { q: search },
                success: function() {
                    cliqFlip.App.Mvc.vent.trigger("feedlist:retrieved", feedList);
                }
            });
        }
    };

    cliqFlip.App.Mvc.vent.bind("interest:searched", function(search) { searchController.doSearch(search); });

    //Router
    cliqFlip.App.Mvc.Routers.SearchRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
        },
        controller: searchController
    });
    return cliqFlip;
} (CliqFlip));