var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Views.AddMediaView = Backbone.Marionette.ItemView.extend({
        className: "add-media",
        template: "landing-addMedia",
        events: {
            "click .btn.btn-primary": "saveMedium",
            "click .dropdown-menu a": "selectDropdownItem"
        },
        onShow: function() {
            $("#photo-upload").html5_upload({
                url: '/search/upload',
                autostart: false //https://github.com/mihaild/jquery-html5-upload
            });
        },
        saveMedium: function(e) {
            $(e.target).attr("disabled", "disabled");
        },
        selectDropdownItem: function(e) {
            this.$("#selected-interest-type").text($(e.target).text());
        }
    });

    return cliqFlip;
} (CliqFlip));