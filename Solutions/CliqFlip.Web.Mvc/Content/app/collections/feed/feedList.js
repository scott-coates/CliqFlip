// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/feed/feedItemModel.js

var CliqFlip = (function(cliqFlip) {

    function incrementPage() {
        this.page = this.page || 1;
        this.page++;
    }

    cliqFlip.App.Mvc.Collections.FeedList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.FeedItemModel,
            url: '/api/feed',
            page: null,
            fetch: function(options) {
                options = options || { };
                options.data = options.data || { };
                if (this.page) options.data.page = this.page;
                incrementPage.call(this);
                return Backbone.Collection.prototype.fetch.call(this, options);
            },
            parse: function(response) {
                return response.data;
            },
            resetPage: function () {
                this.page = null;
            }
        });

    return cliqFlip;
}(CliqFlip));