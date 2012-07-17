// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.AddMedium = Backbone.Model.extend({
        urlRoot: '/search/addmedium',
        saveMedium: function(data, options) {
            this.save(data, options);
        }
    });

    return cliqFlip;
}(CliqFlip));