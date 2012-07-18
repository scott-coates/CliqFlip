var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediumView = Backbone.Marionette.ItemView.extend({
        initialize: function() {
            Backbone.Validation.bind(this);
        },
        className: "add-media",
        template: function(arg) {
            if(arg.MediumType === 'status') {
                return ["landing-addStatus"];
            }
            else {
                var template = ["landing-addMedium"];
                template.push({ MediumType: "landing-add" + arg.MediumType[0].toUpperCase() + arg.MediumType.substring(1) });
                return template;
            }
        },
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
            this.model.set({
                Description: description,
                InterestId: interestId,
                Url: url
            }, { silent: true });

            this.model.saveMedium(
                {
                    success: function() {
                        cliqFlip.App.Mvc.Views.Helpers.alert({ type: 'success', header: 'success', message: that.model.get('MediumType') + ' added' });
                        that.trigger('medium:added');
                    },
                    error: function(model, error) {
                        $(e.target).removeClass('disabled');
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
            that.model.set('FileName', file.name, { silent: true });
            var fileReader = new window.FileReader();
            fileReader.onload = function(theFile) {
                that.model.set('FileData', theFile.target.result, { silent: true });
            };
            fileReader.readAsDataURL(file);
        }
    });

    return cliqFlip;
} (CliqFlip));