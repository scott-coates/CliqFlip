// @reference PostOverviewUserAcitivtyView.js
var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyListView = Backbone.Marionette.CollectionView.extend({
        itemView: cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyView
    });

    return cliqFlip;
} (CliqFlip));