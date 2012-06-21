var CliqFlip = (function(cliqFlip) {

    cliqFlip.Mvc.App.Views.ContentView = Backbone.Marionette.ItemView.extend({
        template: "home-content",
        className: "home-content"
    });

    return cliqFlip;
}(CliqFlip));