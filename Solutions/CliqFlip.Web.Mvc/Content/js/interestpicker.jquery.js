(function ($) {
    $.widget("cliqflip.interestpicker", {

        // These options will be used as defaults
        options: {
            clear: null,
            source: "/Subject/KeywordSearch"
        },

        // Set up the widget
        _create: function () {

            var textBox = this.element;
            var selectionsList = $("#interestsList");
            var itemTemplate = $("#tmplNewInterest");
            var autocompleteList;
            textBox.autocomplete(
                {
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        var term = request.term;

                        var addToList = true;

                        //make a call to the subject search
                        //If the item the user has typed does not have an exact match
                        //it should add it to the end of the list by pushing a new json object into the subject array

                        //Then let autocomplete do it's thing
                        $.getJSON("/Subject/KeywordSearch?input=" + term, function (data) {
                            for (var counter = 0; counter < data.length; counter++) {
                                if (data[counter].Name.toLowerCase() == term.toLowerCase()) {
                                    addToList = false;
                                }
                            }

                            if (addToList) {
                                data.push({ Name: term, Id: 0 });
                            }
                            response(data);
                        });
                    },
                    select: function (event, ui) {
                        var data = $.extend({}, { Index: selectionsList.find("li").length }, ui.item);
                        var newItem = itemTemplate.tmpl(data);
                        newItem.data("item", ui.item)
                        newItem.appendTo(selectionsList);
                        newItem.fadeIn();
                        this.value = "";
                        return false;
                    }
                })
                .data("autocomplete")._renderItem = function (ul, item) {
                    return $("<li></li>")
				    .data("item.autocomplete", item)
				    .append("<a>" + item.Name + "<br>" + item.Id + "</a>")
				    .appendTo(ul);
                };

            autocompleteList = textBox.autocomplete("widget");

        },


        // Use the destroy method to clean up any modifications your widget has made to the DOM
        destroy: function () {
            // In jQuery UI 1.8, you must invoke the destroy method from the base widget
            $.Widget.prototype.destroy.call(this);
            // In jQuery UI 1.9 and above, you would define _destroy instead of destroy and not call the base method
        }
    });
} (jQuery));