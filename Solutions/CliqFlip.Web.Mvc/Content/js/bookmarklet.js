
(function (theWindow, theOptions) {

    var viewGenerator = function () {
        var mainContainer;
        var background;
        var contentContainer;
        var closer;

        function createBackground() {
            //create the main container for bookmarklet
            mainContainer = jQuery("<div></div>")
                                .appendTo("body");

            //create the background. Since we will give the background an opacity, we can't place elements inside of it.
            //If we do, all the child elements will also have an opacity and it can't be removed or overwriten.
            //so make an empty div that stretches the whole screen
            background = jQuery("<div style='width: 100%;height: 100%;background-color: blue;position: fixed;top: 0;left: 0;opacity: 0.6;z-index: 999;'></div>")
                                .appendTo(mainContainer);

            //create the container that will hold all the elements
            contentContainer = jQuery("<div style='width: 80%;height: 80%;background-color: white;position: fixed;top: 15%;left: 10%;z-index: 1000;overflow-y: scroll;'></div>")
                                .appendTo(mainContainer);

            //create the close button and add it to the content container
            closer = jQuery("<div style='width: 100%;height: 10%;background-color: black;position: fixed;top: 0;left: 0;z-index: 1000;'>Close<div>")
                                .appendTo(contentContainer)
                                .click(function () {
                                    mainContainer.remove();
                                });
        }

        function createImageStructure(url) {
            var containerSize = 200;
            var containerHtml = "<div style='float: left; width: " + containerSize + "px;height: " + containerSize + "px;background-color: white;margin: 10px;border: 1px solid;padding: 10px;'>" +
                                "</div>";
            var div = jQuery(containerHtml).click(function () {
                //an image is going to inside the
                alert($(this).find("img")[0].src);
            });

            jQuery("<img src='" + url + "' style='max-height: 100%;max-width: 100%;margin: 0 auto;display: block;' />").appendTo(div);
            return div;
        }

        return {
            showSplashScreen: function () {
                createBackground();
            },
            showImages: function (images) {
                createBackground();
                for (var i = 0; i < images.length; i++) {
                    var imageUrl = images[i];
                    createImageStructure(imageUrl).appendTo(contentContainer);
                }
            },
            closeSplashScreen: function () {
                background.remove();
            }
        };
    } ();

    var jDocParser = function (w) {
        //privates
        //working window
        var ww = w;
        return {
            //public
            getImages: function () {
                var imagesTags = jQuery("img");
                var imgUrls = jQuery.grep(imagesTags, function (image, index) { //remove small images
                    var minWidth = 100, minHeight = 100;
                    //only images bigger than the min size and that have a src
                    return image.width >= minWidth && image.height >= minHeight && image.src;
                }).map(function (image, index) {
                    //this is the image tag
                    return image.src;
                });
                return imgUrls;
            }
        }
    } (theWindow);

    //I want this plug in to be developed using OO practices.
    //I'm experimenting with OO javascript and closures.

    //we need a bookmarklet object
    //that will serve as the main entry point for the mini-application
    var bookmarklet = function (documentParser) {

        //the objects public functions
        return {
            init: function () {

            }
        }
    } (jDocParser); // execute the function so it creates the bookmarklet object




    // the minimum version of jQuery we want
    var v = "1.3.2";

    // check prior inclusion and version
    if (window.jQuery === undefined || window.jQuery.fn.jquery < theOptions.jQueryMinVersion) {
        //jQuery is not available

        var done = false;

        var script = document.createElement("script");
        script.src = "http://ajax.googleapis.com/ajax/libs/jquery/" + theOptions.jQueryDesiredVersion + "/jquery.min.js";
        script.onload = script.onreadystatechange = function () {
            if (!done && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) {
                done = true;
                viewGenerator.showImages(jDocParser.getImages());
            }
        };
        document.getElementsByTagName("head")[0].appendChild(script);
    } else {
        viewGenerator.showImages(jDocParser.getImages());
    }

    //tell bookmarklet to initialize
    //bookmarklet.init();
    //viewGenerator.showImages(jDocParser.getImages());

})(window, {
    minSize: 75,
    jQueryMinVersion: "1.3.2",
    jQueryDesiredVersion: "1.7.1",
    thumbnailSize: 200,
    endpoint: "http://localhost:51949/popup/bookmarkmedium?"
});




//javascript: void ((function () { var e = document.createElement('script'); e.setAttribute('type', 'text/javascript'); e.setAttribute('charset', 'UTF-8'); e.setAttribute('src', 'http://localhost:51949/Content/js/bookmarklet.js?r=' + Math.random() * 99999999); document.body.appendChild(e) })());