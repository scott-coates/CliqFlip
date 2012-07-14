// @reference feedItemView.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedListView = Backbone.Marionette.CompositeView.extend({
        template: "feed-feedList",
        className: 'invisible',
        triggerPoint: 200,
        itemView: cliqFlip.App.Mvc.Views.FeedItemView,
        initialize: function() {
            // isLoading is a useful flag to make sure we don't send off more than
            // one request at a time
            //            http: //stackoverflow.com/questions/9110060/how-do-i-add-a-resize-event-to-the-window-in-a-view-using-backbone
            this.isLoading = false;
            $(window).bind("scroll", null, _.bind(this.checkScroll, this));
        },
        checkScroll: function() {
            if(!this.isLoading && ($(window).scrollTop() >= $(document).height() - $(window).height() - this.triggerPoint)) {
                this.loadResults();
            }
        },
        loadResults: function() {
            var that = this;
            this.isLoading = true;
            this.collection.fetch({
                add: true,
                success: function(models) {
                    that.isLoading = false;
                    that.$el.masonry('reload');
                }
            });
        },
        onShow: function() {
            $(".user-info", this.$el)
                .popover({
                    content: function() {
                        return "Hello " + $(this).data('userid');
                    },
                    delay:{hide:1000, show:250}
                });

            var that = this;
            this.$el.imagesLoaded(function() {
                that.$el.masonry({
                    itemSelector: ".feed-item"
                });
                that.$el.removeClass("invisible");
            });
        }
    });

    return cliqFlip;
} (CliqFlip));