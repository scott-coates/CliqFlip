// @reference ~/Content/app/views/feed/feedItemView.js

var CliqFlip = (function (cliqFlip) {

    cliqFlip.Mvc.App.Views.FeedListView = Backbone.Marionette.CompositeView.extend({
        template: "feed-feedList",
        className: 'feed-list hero-unit',
        itemView: cliqFlip.Mvc.App.Views.FeedItemView,
        onShow: function () {
            var that = this;
            this.$el.imagesLoaded(function () {
                that.$el.masonry({
                    itemSelector: "." + that.itemView.prototype.className,
                    columnWidth: 100
                });
            });
        }
    });

    return cliqFlip;
} (CliqFlip));