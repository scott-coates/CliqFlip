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
        className: "interest-feed-item box-grey",
		render: function () {
			var jsonModel = this.model.toJSON();
			this.$el.html(window.JST['media-' + jsonModel.MediumType].render({ model: jsonModel },
				{ video: window.JST['media-Video'] }));
			return this;
		}			
	});

	//start app - anything before this could be in its own file
	var view = new CliqFlipMVC.Views.Feed.InterestFeedList({
		el: $("#interest-feed"),
		collection: new CliqFlipMVC.Collections.Feed.InterestFeedCollection()
	});
	
	view.render();
}