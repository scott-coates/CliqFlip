var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediumView = Backbone.Marionette.ItemView.extend({
        className: "add-media",
        template: "landing-addMedium",
        events: {
            "click .btn.btn-primary:not(.disabled)": "saveMedium",
            "click .dropdown-menu a": "selectDropdownItem",
            "change #file-upload": "uploadFile"
        },
        saveMedium: function(e) {
            var file = this.$("#file-upload")[0].files[0];
            var description = this.$("#medium-description").val();
            var url = this.$("#medium-url").val();
            var interestId = this.$("#selected-interest-type").data('interestId');
            var that = this;
            if (this.model.set(
                {
                    ImageData: file,
                    Description: description,
                    InterestId: interestId,
                    Url: url
                },
                {
                    error: function(model, error) {
                        that.$(".alert").text(error).removeClass("hide");
                    }
                })) {

                $(e.target).addClass('disabled');
                this.model.saveMedium();
            }

        },
        selectDropdownItem: function(e) {
            var target = $(e.target);
            this.$("#selected-interest-type").text(target.text()).data('interestId', target.data('interestId'));
        },
        uploadFile: function(parameters) {
            var x = document.getElementById("file-upload")
        }
    });

    return cliqFlip;
}(CliqFlip));