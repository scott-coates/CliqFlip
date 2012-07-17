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
            var description = this.$("#medium-description").val();
            var url = this.$("#medium-url").val();
            var interestId = this.$("#selected-interest-type").data('interestId');
            var that = this;

            $(e.target).addClass('disabled');

            this.model.saveMedium({
                    Description: description,
                    InterestId: interestId,
                    Url: url
                },
                {
                    success: function() {
                        cliqFlip.App.Mvc.Views.Helpers.alert({ type: 'success', header: 'success', message: 'medium added' });
                        that.close();
                    },
                    error: function(model, error) {
                        that.$(".alert").text(error).removeClass("hide");
                    }
                });
        },
        selectDropdownItem: function(e) {
            var target = $(e.target);
            this.$("#selected-interest-type").text(target.text()).data('interestId', target.data('interestId'));
        },
        uploadFile: function(e) {
            var that = this;
            var fileUplad = e.target;
            var file = fileUplad.files[0];
            var fileReader = new window.FileReader();
            fileReader.onload = function(theFile) {
                that.model.set('ImageData', theFile.target.result);
            };
            fileReader.readAsBinaryString(file);
        }
    });

    return cliqFlip;
}(CliqFlip));