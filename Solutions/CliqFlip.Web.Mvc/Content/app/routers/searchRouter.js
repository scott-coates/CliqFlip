//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var searchController = {
        doSearch: function(search) {
            cliqFlip.App.Mvc.vent.trigger("interest:searched:keyword", search);
        }
    };

    cliqFlip.App.Mvc.vent.bind("interest:searched", function(search) {
        if ($.trim(search).length > 0) {
            searchController.doSearch(search);
            cliqFlip.App.Mvc.searchRouter.navigate("search?q=" + search);
        }
    });

    //Router
    cliqFlip.App.Mvc.Routers.SearchRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
                        
        },
        controller: searchController
    });
    return cliqFlip;
}(CliqFlip));