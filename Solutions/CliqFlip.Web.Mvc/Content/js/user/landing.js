var CliqFlipMVC = CliqFlipMVC || { };
CliqFlipMVC.Models = CliqFlipMVC.Models || { };
CliqFlipMVC.Models.Feed = { };
CliqFlipMVC.Collections = CliqFlipMVC.Collections || { };
CliqFlipMVC.Collections.Feed = { };
CliqFlipMVC.Views = CliqFlipMVC.Views || { };
CliqFlipMVC.Views.Feed = { };

function InitFeed(feedUrl) {
	CliqFlipMVC.Models.Feed.InterestFeed = Backbone.Model.extend({
				
	});

	(function() {

		function incrementPage() {
			this.page = this.page || 1;
			this.page++;
		}

		CliqFlipMVC.Collections.Feed.InterestFeedCollection = Backbone.Collection.extend({
			model: CliqFlipMVC.Models.Feed.InterestFeed,
			url: feedUrl,
			page: null,
			fetch: function(options) {
				options = options || { };
				if (this.page) {
					options.data = options.data || { };
					options.data.page = this.page;
				}
				incrementPage.call(this);
				return Backbone.Collection.prototype.fetch.call(this, options);
			}
		});
	})(); //anonymous function to hide the function to increment page

	CliqFlipMVC.Views.Feed.InterestFeedList = Backbone.View.extend({
		initialize: function() {
			// isLoading is a useful flag to make sure we don't send off more than
			// one request at a time
			this.isLoading = false;
		},
		loadResults: function() {
			var that = this;
			this.isLoading = true;
			this.collection.fetch({
				success: function(models) {
					_.each(that.collection.models, function (model) {
						that.$el.append(new CliqFlipMVC.Views.Feed.InterestFeedItem({ model: model }).render().el);
					});
					that.isLoading = false;
				}
			});
		},
		events: {
			'scroll': 'checkScroll'
		},
		checkScroll: function() {
			var triggerPoint = 300;
			if (!this.isLoading && this.el.scrollTop + this.el.clientHeight + triggerPoint > this.el.scrollHeight) {
				this.loadResults();
			}
		},
		render: function() {
			this.loadResults();
		}
	});

	CliqFlipMVC.Views.Feed.InterestFeedItem = Backbone.View.extend({

	    events: {
	        "click": "showDetailedView"
	    },
	    render: function () {
	        var jsonModel = this.model.toJSON();
	        this.$el.html(window.JST['media-' + jsonModel.MediumType].render({ model: jsonModel },
				{ video: window.JST['media-Video'] }))
                .find("div.detailed-content").empty();
	        return this;
	    },
	    showDetailedView: function (event) {

	        //the detailed view will be basically the as it shows on the feed
	        //but will show some extra things. I will have the video/iframe/or big image

	        //if this element is clicked when it's inside the 
	        //color box ignore the click
	        if (this.$el.parents("#colorbox").length || $(event.srcElement).is("a")) {
	            return;
	        }

	        var that = this;

	        if (this.$el.find("div.detailed-content").is(":empty")) {
	            //to prevent loading a bunch of hidden iframe's and having them
	            //pre-load, insert the iframe into the right place when the item is clicked
	            var detailedContent;
	            switch (this.model.get('MediumType')) {
	                case "Video":
	                    detailedContent = "<iframe src=" + this.model.get("VideoUrl") + "/>";
	                    break;
	                case "WebPageNoImage":
	                case "WebPage":
	                    detailedContent = "<iframe src=" + this.model.get("WebSiteUrl") + "/>";
	                    break;
	            }
	            this.$el.find(".detailed-content").html(detailedContent);
	        }


	        //show the color box
	        $.colorbox({ inline: true, 
                href: this.$el, 
                open: true,
	            onCleanup: function () {
	                //we need to clear the detailed contents or the
	                //browser will end up loading the iframe as the modal is closed
	                that.$el.find(".detailed-content").empty();
	            },
	            width: 800,
	            height: "90%"
	        });
	    }
	});


	//start app - anything before this could be in its own file
	var view = new CliqFlipMVC.Views.Feed.InterestFeedList({
		el: $("#interest-feed"),
		collection: new CliqFlipMVC.Collections.Feed.InterestFeedCollection()
	});
	
	view.render();
}