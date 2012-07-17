// @reference ~/Content/app/main.js
// @reference ~/Content/app/models/landing/feedItem.js

var CliqFlip = (function (cliqFlip) {

    function incrementPage() {
        this.page = this.page || 1;
        this.page++;
    }

    cliqFlip.App.Mvc.Collections.FeedList = Backbone.Collection.extend(
        {
            model: cliqFlip.App.Mvc.Models.FeedItem,
            url: '/search/interestfeed',
            page: null,
            fetch: function(options) {
                options = options || {};
                if(this.page) {
                    options.data = options.data || {};
                    options.data.page = this.page;
                }
                incrementPage.call(this);
                return Backbone.Collection.prototype.fetch.call(this, options);
            }
        });

    return cliqFlip;
} (CliqFlip));