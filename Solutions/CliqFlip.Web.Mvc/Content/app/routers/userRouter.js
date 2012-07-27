//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var userController = {
        index: function(user) {
            alert(user);
        }
    };

    cliqFlip.App.Mvc.vent.bind("userItem:selected", function(user) { userController.index(user); });

    //Router
    cliqFlip.App.Mvc.Routers.UserRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "users/:username": "index"
            //            "users/:username": "index"
        },
        controller: userController
    });
    return cliqFlip;
} (CliqFlip));