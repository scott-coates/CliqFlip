
 /* bootstrap-multitypeahead.js v0.0.1
 * http://twitter.github.com/bootstrap/javascript.html#typeahead
 * =============================================================
 * Copyright 2012 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ============================================================ */


!function ($) {

    "use strict"; // jshint ;_;


    /* TYPEAHEAD PUBLIC CLASS DEFINITION
    * ================================= */

    //start off by copying bootstrap typeahead
    var MultiTypeahead = $.fn.typeahead.Constructor;
    //MultiTypeahead.prototype = $.fn.typeahead.Constructor.prototype;

    //overide a few methods
    MultiTypeahead.prototype.constructor = MultiTypeahead;
    MultiTypeahead.prototype.select = function () {
        var val = this.$menu.find('.active').attr('data-value');

        //get the values in the textbox
        //and tokenize them
        var tokens = this.split(this.$element.val());

        // remove the current input
        tokens.pop();

        //add the selected item
        tokens.push(val);

        // add placeholder to get the comma-and-space at the end
        tokens.push("");
        var replacement = tokens.join(", ");

        this.$element.val(this.updater(replacement)).change();
        return this.hide();
    };
    MultiTypeahead.prototype.split = function (val) {
        return val.split(/,\s*/);
    };
    MultiTypeahead.prototype.extractLast = function (term) {
        return this.split(term).pop();
    };
    MultiTypeahead.prototype.lookup = function (event) {
        var that = this, items, q;

        this.query = this.$element.val();

        if (!this.query) {
            return this.shown ? this.hide() : this;
        }

        this.query = this.extractLast(this.$element.val());

        items = $.grep(this.source, function (item) {
            return that.matcher(item);
        });

        items = this.sorter(items);

        if (!items.length) {
            return this.shown ? this.hide() : this;
        }

        return this.render(items.slice(0, this.options.items)).show();
    };


    /* TYPEAHEAD PLUGIN DEFINITION
    * =========================== */

    $.fn.multitypeahead = function (option) {
        return this.each(function () {
            var $this = $(this),
                data = $this.data('multitypeahead'),
                options = typeof option == 'object' && option;

            if (!data) {
                $this.data('multitypeahead', (data = new MultiTypeahead(this, options)));
            }
            if (typeof option == 'string') {
                data[option]();
            }
        });
    };

    $.fn.multitypeahead.Constructor = MultiTypeahead;


    /* TYPEAHEAD DATA-API
    * ================== */

    $(function () {
        $('body').on('focus.multitypeahead.data-api', '[data-provide="multitypeahead"]', function (e) {
            var $this = $(this);
            if ($this.data('multitypeahead')) {
                return;
            }
            e.preventDefault();
            $this.multitypeahead($this.data());
        });
    });

} (window.jQuery);