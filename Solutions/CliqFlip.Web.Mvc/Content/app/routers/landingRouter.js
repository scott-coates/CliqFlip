//https: //github.com/derickbailey/backbone.marionette/blob/master/docs/marionette.approuter.md

var CliqFlip = (function (cliqFlip) {
    var landingController = {
        landing: function () {
            /*
            look into filters:
            https://github.com/angelo0000/backbone_filters/blob/master/backbone_filters.js
            https://github.com/documentcloud/backbone/pull/299'
            http://coenraets.org/blog/2012/01/backbone-js-lessons-learned-and-improved-sample-app/
            */
            if (cliqFlip.App.Mvc.modalRegion.currentView) {
                cliqFlip.App.Mvc.modalRegion.hideModal();
            }

            if (!cliqFlip.App.Mvc.mainContentRegion.currentView) {

                var landingLayout = new cliqFlip.App.Mvc.Layouts.LandingLayout();

                cliqFlip.App.Mvc.mainContentRegion.show(landingLayout);

                var userLandingSummaryModel = new cliqFlip.App.Mvc.Models.UserLandingSummary(cliqFlip.App.UserData);

                var userLandingSummaryView = new cliqFlip.App.Mvc.Views.UserLandingSummaryView({ model: userLandingSummaryModel });
                landingLayout.leftColumnRegion.show(userLandingSummaryView);

                var feedList = new cliqFlip.App.Mvc.Collections.FeedList();

                feedList.fetch({
                    success: function () {
                        landingLayout.contentAreaRegion.show(new cliqFlip.App.Mvc.Views.FeedListView({ collection: feedList }));
                    }
                });
            }
        },
        showPost: function (post) {
            var postOverview = new cliqFlip.App.Mvc.Models.FeedPostOverview({ id: post.get('PostId') });
            postOverview.fetch({
                success: function (model) {
                    var postOverviewLayout = new cliqFlip.App.Mvc.Layouts.FeedPostOverviewLayout({ model: model });
                    cliqFlip.App.Mvc.modalRegion.show(postOverviewLayout);

                    var activityModels = _.map(model.get('Activity'), function (parameters) {
                        return new cliqFlip.App.Mvc.Models.FeedPostOverviewUserActivity(parameters);
                    });
                    var postCollection = new cliqFlip.App.Mvc.Collections.FeedPostOverviewUserActivityList(activityModels);
                    var activityListView = new cliqFlip.App.Mvc.Views.PostOverviewUserAcitivtyListView({ collection: postCollection });

                    postOverviewLayout.userActivityRegion.show(activityListView);


                    var contentTemplate = "media-Image"
                    if (model.get("VideoUrl")) {
                        contentTemplate = "media-Video";
                    } else if (model.get("WebPageUrl")) {
                        contentTemplate = "media-WebPage";
                    }


                    var postContentView = new cliqFlip.App.Mvc.Views.FeedItemContentView({
                        model: model,
                        template: contentTemplate
                    });

                    postOverviewLayout.contentRegion.show(postContentView);
                }
            });
        }
    };

    cliqFlip.App.Mvc.Routers.LandingRouter = Backbone.Marionette.AppRouter.extend({
        appRoutes: {
            "": "landing"
        },
        controller: landingController
    });

    cliqFlip.App.Mvc.vent.bind("feedItem:selected", function (post) { landingController.showPost(post); });
    cliqFlip.App.Mvc.modalRegion.on("view:closed", function (view) {
        cliqFlip.App.Mvc.landingRouter.navigate("");
    });


    return cliqFlip;
} (CliqFlip));