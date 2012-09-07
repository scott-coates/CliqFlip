var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddPostView = Backbone.Marionette.ItemView.extend({
        initialize: function() {
            Backbone.Validation.bind(this);
        },
        className: "add-media",
        template: function(arg) {
            if(arg.PostType === 'status') {
                return ["landing-addStatus"];
            }
            else {
                var template = ["landing-addPost"];
                template.push({ PostType: "landing-add" + arg.PostType[0].toUpperCase() + arg.PostType.substring(1) });
                return template;
            }
        },
        events: {
            "click .btn.btn-primary:not(.disabled)": "savePost",
            "click .dropdown-menu a": "selectDropdownItem",
            "change #file-upload": "uploadFile"
        },
        savePost: function(e) {
            var description = this.$("#post-description").val();
            var url = this.$("#post-url").val();
            var interestId = this.$("#selected-interest-type").data('interestId');
            var that = this;

            $(e.target).addClass('disabled');

            this.model.set({
                Description: description,
                InterestId: interestId
            }, { silent: true });

            if(url) {
                this.model.set("PostData", url, { silent: true });
            }

            this.model.savePost(
                {
                    success: function() {
                        cliqFlip.App.Mvc.Views.Helpers.alert({ type: 'success', header: 'success', message: that.model.get('PostType') + ' added' });
                        that.trigger('post:added');
                    },
                    error: function(model, error) {
                        $(e.target).removeClass('disabled');
                        that.$(".alert").text(error.statusText || error).removeClass("hide");
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
                that.model.set('PostData', theFile.target.result, { silent: true });
            };
            fileReader.readAsDataURL(file);
        }
    });

    return cliqFlip;
} (CliqFlip));