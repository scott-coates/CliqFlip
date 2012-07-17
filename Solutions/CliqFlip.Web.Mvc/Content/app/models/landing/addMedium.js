// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.AddMedium = Backbone.Model.extend({
        urlRoot: '/search/addmedium',
        defaults: {
            ImageData: undefined
        },
        saveMedium: function(options) {
            this.save(this.attributes, options);
        },
        validation: {
            InterestId: {
                required: true,
                msg: 'Interest is required'
            },
            ImageData: {
                required: true,
                msg: 'ImageData is required'
            }
        }
    });

    return cliqFlip;
}(CliqFlip));