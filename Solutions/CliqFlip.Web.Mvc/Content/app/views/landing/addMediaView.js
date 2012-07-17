var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediaView = Backbone.Marionette.ItemView.extend({
        className: "add-media",
        template: "landing-addMedia",
        events: {
            "click .btn.btn-primary": "saveMedium"
        },
        onShow: function() {
            $("#photo-upload").html5_upload({
                url:'/search/upload',
                autostart: false //https://github.com/mihaild/jquery-html5-upload
            });
        },
        saveMedium: function() {
            this.$('#photo-upload').fileupload().submit();
        }
    });

    return cliqFlip;
} (CliqFlip));