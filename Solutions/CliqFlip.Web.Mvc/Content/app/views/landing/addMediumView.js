var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediumView = Backbone.Marionette.ItemView.extend({
        className: "add-media",
        template: "landing-addMedium",
        events: {
            "click .btn.btn-primary:not(.disabled)": "saveMedium",
            "click .dropdown-menu a": "selectDropdownItem"
        },
        saveMedium: function(e) {
            $(e.target).addClass('disabled');
            var formData = new window.FormData(this.$('form')[0]);
            var description = this.$("#medium-description").val();
            var url = this.$("#medium-url").val();
            var interestId = this.$("#selected-interest-type").data('interestId');
            this.model.saveMedium({ imageData: formData, description: description, interestId: interestId, url: url });
        },
        selectDropdownItem: function(e) {
            var target = $(e.target);
            this.$("#selected-interest-type").text(target.text()).data('interestId', target.data('interestId'));
        }
    });

    return cliqFlip;
}(CliqFlip));