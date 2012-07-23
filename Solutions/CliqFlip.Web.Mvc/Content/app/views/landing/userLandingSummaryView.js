var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.UserLandingSummaryView = Backbone.Marionette.ItemView.extend({
        initialize: function() {
            this.bindTo(cliqFlip.App.Mvc.vent, "user:selection:changed", this.changeSelectionTab);
        },
        template: "landing-userLandingSummary",
        changeSelectionTab: function(model) {
            this.$(".user-selection").removeClass('active');
            this.$("#" + model + "-selection").addClass('active');
        }
    });

    return cliqFlip;
} (CliqFlip));