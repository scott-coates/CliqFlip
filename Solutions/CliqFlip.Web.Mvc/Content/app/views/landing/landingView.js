var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Views.LandingView = Backbone.Marionette.ItemView.extend({
        template: "landing-content",
        className: "container gray-rounded-border",
        showFeed: function() {
            cliqFlip.Mvc.App.vent.trigger("feed:showList");
        }
    });

    return cliqFlip;
}(CliqFlip));