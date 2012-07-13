var CliqFlip = (function (cliqFlip) {

    cliqFlip.App.Mvc.Views.SearchView = Backbone.View.extend({
        el: $("#search-container"),
        initialize: function () {
            this.render();
        },
        render: function () {
            var mtypeahead = this.$el.find("input").multitypeahead({
                source: function (term, response) {
                    $.getJSON("/Search/Interest?input=" + term, function (data) {
                        response(_.pluck(data, 'Name'));
                    });
                }
            });

            mtypeahead.data('multitypeahead').$menu.attr("id", "search-suggestions");

            return this;
        }
    });

    return cliqFlip;
} (CliqFlip));