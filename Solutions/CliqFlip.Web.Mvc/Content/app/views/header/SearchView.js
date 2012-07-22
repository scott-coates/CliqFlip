var CliqFlip = (function(cliqFlip) {
    cliqFlip.App.Mvc.Views.SearchView = Backbone.Marionette.ItemView.extend({
        template: "header-search",
        className: "offset2 span7",
        onShow: function() {
            var mtypeahead = this.$("#search-box").multitypeahead({
                source: function(term, response) {
                    $.getJSON("/api/Interest?interestName=" + term, function(data) {
                        response(_.pluck(data, 'Name'));
                    });
                }
            });

            mtypeahead.data('multitypeahead').$menu.attr("id", "search-suggestions").css("min-width", mtypeahead.width());

            return this;
        },
        events: {
            "click #btn-search": "search"
        },
        search: function() {
            cliqFlip.App.Mvc.vent.trigger("interest:searched", this.$("#search-box").val());
        }
    });

    return cliqFlip;
}(CliqFlip));