//http://lostechies.com/derickbailey/2012/04/17/managing-a-modal-dialog-with-backbone-and-marionette/
var CliqFlip = (function(cliqFlip) {
    cliqFlip.View = cliqFlip.View || {};

    cliqFlip.View.ModalRegion = Backbone.Marionette.Region.extend({
        constructor: function() {
            Backbone.Marionette.Region.prototype.constructor.apply(this, arguments);
            this.on("view:show", this.showModal, this);
        },

        getEl: function(selector) {
            var $el = $(selector);
            $el.on("hidden", _.bind(this.close, this));
            return $el;
        },

        showModal: function() {
            this.$el.modal('show');
        },

        hideModal: function() {
            this.$el.modal('hide');
        },

        open: function(view) {
            this.$el.addClass(this.firstClassName(view.className) + "-modal");
            Backbone.Marionette.Region.prototype.open.apply(this, arguments);
        },

        close: function() {
            var view = this.currentView;
            if(!view) { return; }
            this.$el.removeClass(this.firstClassName(this.currentView.className) + "-modal");
            Backbone.Marionette.Region.prototype.close.apply(this, arguments);
        },

        firstClassName: function(className) {
            return className.split(' ')[0];
        }
    });

    return cliqFlip;
} (CliqFlip || {}));