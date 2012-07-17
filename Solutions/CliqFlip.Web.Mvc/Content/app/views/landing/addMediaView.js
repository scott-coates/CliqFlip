var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediaView = Backbone.Marionette.ItemView.extend({
        className: "add-media",
        template: "landing-addMedia"
    });

    return cliqFlip;
} (CliqFlip));