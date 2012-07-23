var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.UserLandingSummaryView = Backbone.Marionette.ItemView.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "user:selection:changed", this.changeSelectionTab);
        },
        template: "landing-userLandingSummary",
        events: {
            "click .user-selection": "changingSelection"
        },
        changeSelectionTab: function(model) {
            this.$(".user-selection").removeClass('active');
            this.$("#" + model + "-selection").addClass('active');
        },
        changingSelection: function(e) {
            var selection = $(e.target).data('selection');
            cliqFlip.App.Mvc.vent.trigger("user:selection:changing:" + selection);
        }
    });

    return cliqFlip;
} (CliqFlip));