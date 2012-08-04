//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function(cliqFlip) {
    var landingController = {
        index: function() {
            this.showLanding(true);
        },
        showLanding: function(showFeedList) {
            /*
            look into filters:
            https://github.com/angelo0000/backbone_filters/blob/master/backbone_filters.js
            https://github.com/documentcloud/backbone/pull/299'
            http://coenraets.org/blog/2012/01/backbone-js-lessons-learned-and-improved-sample-app/
            */
            if(cliqFlip.App.Mvc.modalRegion.currentView) {
                cliqFlip.App.Mvc.modalRegion.hideModal();
            }

            cliqFlip.App.Mvc.vent.trigger("user:selection:changed", "feed");

            if(!cliqFlip.App.Mvc.landingRegion.currentView) {

                this.shouldRefreshLanding = false;

                var landingLayout = new cliqFlip.App.Mvc.Layouts.LandingLayout();

                cliqFlip.App.Mvc.landingRegion.show(landingLayout);
                $(cliqFlip.App.Mvc.userRegion.el).hide();

                var userLandingSummaryModel = new cliqFlip.App.Mvc.Models.UserLandingSummary(cliqFlip.App.UserData);

                var userLandingSummaryView = new cliqFlip.App.Mvc.Views.UserLandingSummaryView({ model: userLandingSummaryModel });
                landingLayout.userRegion.show(userLandingSummaryView);

                var feedList = new cliqFlip.App.Mvc.Collections.FeedList();
                var feedListView = new cliqFlip.App.Mvc.Views.FeedListView({ collection: feedList });
                landingLayout.contentAreaRegion.show(feedListView);
                if(showFeedList) feedListView.showFeedList();
            }
            else {
                cliqFlip.App.Mvc.landingRegion.currentView.$el.show();
            }
        },
        showPost: function(post) {
            var postOverview = new cliqFlip.App.Mvc.Models.PostOverview({ id: post.get('PostId') });
            postOverview.fetch({
                success: function(model) {
                    var postOverviewLayout = new cliqFlip.App.Mvc.Layouts.PostOverviewLayout({ model: model });
                    cliqFlip.App.Mvc.modalRegion.show(postOverviewLayout);

                    var activityModels = _.map(model.get('Activity'), function(parameters) {
                        return new cliqFlip.App.Mvc.Models.PostOverviewUserActivity(parameters);
                    });
                    var postCollection = new cliqFlip.App.Mvc.Collections.PostOverviewUserActivityList(activityModels);
                    var activityListView = new cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyListView({ collection: postCollection });

                    postOverviewLayout.userActivityRegion.show(activityListView);
                }
            });
        }
    };

    cliqFlip.App.Mvc.vent.bind("interest:searched:validated", function(search) {
        landingController.showLanding(false);
        cliqFlip.App.Mvc.vent.trigger("interest:searched:query", search);
    });

    cliqFlip.App.Mvc.vent.bind("postItem:selected", function(post) { landingController.showPost(post); });
    cliqFlip.App.Mvc.vent.bind("user:selection:changing:feed", function() { cliqFlip.App.Mvc.vent.trigger("user:selection:changed", "feed"); });
    cliqFlip.App.Mvc.modalRegion.on("view:closed", function() {
        cliqFlip.App.Mvc.landingRouter.navigate("");
    });

    //Router
    cliqFlip.App.Mvc.Routers.LandingRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "": "index"
        },
        controller: landingController
    });
    return cliqFlip;
} (CliqFlip));