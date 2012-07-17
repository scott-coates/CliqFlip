var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Views.Helpers = {
        alert: function(options) {
            var jst = window.JST["helpers-alert"];
            cliqFlip.App.Mvc.mainContentRegion.$el.prepend(jst.render(options));
        }
    };
    return cliqFlip;
} (CliqFlip || {}));