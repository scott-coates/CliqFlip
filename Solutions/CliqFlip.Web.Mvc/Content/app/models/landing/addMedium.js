// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.AddMedium = Backbone.Model.extend({
        urlRoot: '/search/addmedium',
        defaults: {
            MediumData: undefined
        },
        saveMedium: function(options) {
            this.save(this.attributes, options);
        },
        validation: {
            InterestId: {
                required: true,
                msg: 'Interest is required'
            },
            MediumData: {
                required: true,
                msg: 'A source is required'
            }
        }
    });

    return cliqFlip;
}(CliqFlip));