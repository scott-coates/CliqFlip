// @reference ~/Content/app/main.js

var CliqFlip = (function(cliqFlip) {

    cliqFlip.App.Mvc.Models.AddMedium = Backbone.Model.extend({
        validate: function(attrs) {
            if (!attrs.InterestId) {
                return 'interest is required';
            }else if(!attrs.ImageData) {
                return 'a file is required';                
            }
        },
        saveMedium: function(parameters) {
            $.ajax({
                url: '/search/photoupload', //http://stackoverflow.com/questions/166221/how-can-i-upload-files-asynchronously-with-jquery
                type: 'POST',
                data: parameters.imageData,
                //Options to tell JQuery not to process data or worry about content-type
                cache: false,
                contentType: false,
                processData: false
            });
        }
    });

    return cliqFlip;
}(CliqFlip));