//https://github.com/derickbailey/backbone.marionette/wiki/Using-jst-templates-with-marionette
Backbone.Marionette.Renderer.render = (function() {

    function renderTemplate(template, data) {

        if (_.isString(template)) {
            return getTemplate(template).render(data);
        }
        else {
            var templateArray = template(data);
            var mainTemplateName = _.head(templateArray);
            var partialTemplateNames = _.tail(templateArray);

            var mainTemplate = getTemplate(mainTemplateName);

            var partialObject = { };

            _.each(partialTemplateNames, function(x) {
                var partialName = _.keys(x)[0];
                var partialTemplate = getTemplate(_.values(x)[0]);
                partialObject[partialName] = partialTemplate;
            });

            return mainTemplate.render(data, partialObject);
        }
    }

    function getTemplate(templateName) {
        if (!window.JST[templateName]) throw "Template '" + templateName + "' not found!";
        return window.JST[templateName];
    }

    return renderTemplate;
})();