var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Views.ContentView = Backbone.Marionette.ItemView.extend({
        template: "home-content",
        className: "home-content",
        events: {
            "click .btn": "showFeed"
        },

        showFeed: function() {
            cliqFlip.Mvc.App.vent.trigger("feed:showList");
        }
    });

    return cliqFlip;
}(CliqFlip));