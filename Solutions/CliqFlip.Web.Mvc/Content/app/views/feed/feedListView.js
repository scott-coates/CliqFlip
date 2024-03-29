// @reference feedItemView.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.FeedListView = Backbone.Marionette.CollectionView.extend({
        triggerPoint: 200,
        data: null,
        itemView: cliqFlip.App.Mvc.Views.FeedItemView,
        initialize: function() {
            // preventLoading is a useful flag to make sure we don't send off more than
            // one request at a time
            //            http: //stackoverflow.com/questions/9110060/how-do-i-add-a-resize-event-to-the-window-in-a-view-using-backbone
            this.preventLoading = false;
            $(window).bind("scroll", null, _.bind(this.checkScroll, this));
            this.bindTo(cliqFlip.App.Mvc.vent, "interest:searched:query", this.doSearch);
            this.bindTo(cliqFlip.App.Mvc.vent, "user:selection:changing:feed", this.showFeedList);
        },
        checkScroll: function() {
            if(!this.preventLoading && ($(window).scrollTop() >= $(document).height() - $(window).height() - this.triggerPoint)) {
                this.loadResults();
            }
        },
        loadResults: function() {
            var that = this;
            this.preventLoading = true;
            this.collection.fetch({
                data: this.data,
                add: true,
                success: function(collection, data) {
                    if(data.returned > 0) {
                        that.$el.imagesLoaded(function() {
                            that.$el.masonry('reload');
                            that.$(".invisible").removeClass("invisible");
                            that.preventLoading = false;                            
                        });
                    }
                }
            });
        },
        onShow: function() {
            //masonry needs to be invoked only after the container is on the page as masonry needs the dimensions
            this.$el.masonry({
                itemSelector: ".feed-item"
            });
        },
        doSearch: function(search) {
            this.data = { q: search };
            this.collection.reset();
            this.collection.resetPage();
            this.loadResults();
        },
        showFeedList: function() {
            this.data = null;
            this.collection.reset();
            this.collection.resetPage();
            this.loadResults();
        }
    });

    return cliqFlip;
} (CliqFlip));