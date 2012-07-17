var CliqFlip = (function (cliqFlip) {
    cliqFlip.App.Mvc.Views.SearchView = Backbone.Marionette.ItemView.extend({
        template: "header-search",
        className: "offset2 span7",
        onShow: function () {
            var mtypeahead = this.$("input").multitypeahead({
                source: function (term, response) {
                    $.getJSON("/Search/Interest?input=" + term, function (data) {
                        response(_.pluck(data, 'Name'));
                    });
                }
            });

            mtypeahead.data('multitypeahead').$menu.attr("id", "search-suggestions").css("min-width", mtypeahead.width());

            return this;
        }
    });

    return cliqFlip;
} (CliqFlip));