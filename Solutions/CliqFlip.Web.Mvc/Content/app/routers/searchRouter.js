//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var searchController = {
        doSearch: function(search) {
            alert('did a search');
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