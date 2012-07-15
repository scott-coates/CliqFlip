var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedItemContentView = Backbone.Marionette.ItemView.extend({
        templateHelpers: cliqFlip.ViewHelpers,
    });

    return cliqFlip;
} (CliqFlip));