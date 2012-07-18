// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.AddPost = Backbone.Model.extend({
        urlRoot: '/search/addpost',
        defaults: {
            PostData: undefined
        },
        savePost: function(options) {
            this.save(this.attributes, options);
        },
        validation: {
            InterestId: {
                required: true,
                msg: 'Interest is required'
            },
            PostData: {
                required: function() {
                    return this.get('PostType') !== 'status';
                },
                msg: 'A source is required'
            }
        }
    });

    return cliqFlip;
} (CliqFlip));