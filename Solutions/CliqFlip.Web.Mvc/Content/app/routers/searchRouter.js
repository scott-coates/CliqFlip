//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var searchController = {
        doSearch: function(search) {
            if ($.trim(search).length > 0) {
                cliqFlip.App.Mvc.vent.trigger("interest:searched:validated", search);
                cliqFlip.App.Mvc.searchRouter.navigate("search?q=" + search);
                cliqFlip.App.Mvc.vent.trigger("user:selection:changed", "search");
            }
        }
    };

    cliqFlip.App.Mvc.vent.bind("interest:searched", function(search) {
        searchController.doSearch(search);
    });

    //Router
    cliqFlip.App.Mvc.Routers.SearchRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "search?q=:search": "doSearch"
        },
        controller: searchController
    });
    return cliqFlip;
}(CliqFlip));