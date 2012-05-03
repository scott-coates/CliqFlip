
(function (theWindow, theOptions) {

	// mediaFinder will look at the document in the window
	// It uses jQuery to retrieve images from the page and then it remove images that are too small
	// In the future this object should also take care of videos.
	var mediaFinder = function () {
		var minSize = theOptions.minSize;
		return {
		//public
			getImages: function() {
				var imagesTags = jQuery("img");

				var imgUrls = jQuery.grep(imagesTags, function(image, index) { //remove small images
					//only images bigger than the min size and that have a src
					//check the natural size of the image not the size on the screen
					return image.naturalWidth >= minSize && image.naturalHeight >= minSize && image.src;
				}).map(function(image, index) {
					//this is the image tag
					return image.src;
				});
				return imgUrls;
			}
		};
	} ();




	var bookmarklet = function () {
		var mainContainer, background, contentContainer, closer, highestZIndex;

		function getTopZIndex() {
			//http://stackoverflow.com/questions/1118198/how-can-you-figure-out-the-highest-z-index-in-your-document
			//to find the highest z-index, we need look at all the elements and compare the computed z-index
			if (highestZIndex) {
				return highestZIndex;
			}
			highestZIndex = 0;
			var bodyElements = theWindow.document.body.getElementsByTagName("*"); // jQuery("body *");
			var i, numOfElements = bodyElements.length;
			for (i = 0; i < numOfElements; i++) {
				var element = bodyElements[i];

				var zIndex = parseInt(theWindow.document.defaultView.getComputedStyle(element, null).getPropertyValue("z-index"), 10);
				if (zIndex && highestZIndex < zIndex) {
					highestZIndex = zIndex;
				}
			}
			return highestZIndex;
		}

		function createMainContainer() {
			var minZIndex = getTopZIndex() + 1;

			//create the main container for bookmarklet
			mainContainer = jQuery("<div></div>")
                                .appendTo("body");
			//add some styles
			jQuery("<style type='text/css'>" +
                    "#bookmarklet_closer:hover{ background-color: #046491; }" +
                    "#bookmarklet_closer{ background-color: #24282D; width: 100%; height: 35px; line-height: 35px; text-align: center; position: fixed; top: 0; left: 0; z-index: 1000; display:block; }" +
                    "div.share-item-container span {position: absolute; bottom: 10px; left: 90px;background-color: #4DBCE9;border: solid 1px;padding: 5px;border-radius: 5px; display: none;}" +
                    "div.share-item-container:hover span {display: block;}" +
                    "#bookmarklet-background {width: 100%;height: 100%;background-color: #24282D;position: fixed;top: 0;left: 0;opacity: 0.6;z-index: " + minZIndex + ";}" +
                    "#bookmarklet-content-container {width: 80%;height: 80%;background-color: white;position: fixed;top: 15%;left: 10%;z-index: 1000;overflow-y: scroll;z-index:" + minZIndex + "}" +
                    "#bookmarklet-content-container img {max-height: 100%;max-width: 100%;margin: 0 auto;display: block;}" +
                    "#bookmarklet_closer{ color: white; }"
                + "</style>").appendTo(mainContainer);

			//create the background. Since we will give the background an opacity, we can't place elements inside of it.
			//If we do, all the child elements will also have an opacity and it can't be removed or overwriten.
			//so make an empty div that stretches the whole screen
			background = jQuery("<div id='bookmarklet-background'></div>")
                                .appendTo(mainContainer);

			//youtube videos don't play nice with the z-index
			//the youtube video will always show on top
			//to overcome this put in iframe that stretches the whole bg
			jQuery("<iframe allowtransparency='true' width='100%' height='100%' />").appendTo(background);

			//create the container that will hold all the elements
			contentContainer = jQuery("<div id='bookmarklet-content-container'></div>")
                                .appendTo(mainContainer);

			//create the close button and add it to the content container
			closer = jQuery("<a id='bookmarklet_closer' href='javascript:void(0)'>Close<a>")
                                .appendTo(contentContainer)
                                .click(function () {
                                	mainContainer.remove();
                                	theWindow.hasAnOpenBookmarklet = false;
                                });

		}

		function createImageStructure(url) {
			var containerSize = theOptions.thumbnailSize;
			var containerHtml = "<div class='share-item-container' style='text-align: center; float: left; width: " + containerSize + "px;height: " + containerSize + "px;background-color: white;margin: 10px;border: 1px solid;padding: 10px;position: relative;'>" +
                                    "<img src='" + url + "' />" +
                                    "<span>Share</span>" +
                                "</div>";
			var div = jQuery(containerHtml).click(function () {
				var imgSrc = jQuery(this).find("img")[0].src;
				window.open(theOptions.endpoint + "?mediumurl=" + imgSrc, "CliqFlip Share", "status=no,resizable=yes,scrollbars=yes,personalbar=no,directories=no,location=no,toolbar=no,menubar=no,width=632,height=270,left=0,top=0");
			});
			return div;
		}

		function loadjQuery(onjQueryLoaded) {
			//check if jquery is in place
			// check prior inclusion and version
			if (theWindow.jQuery === undefined || theWindow.jQuery.fn.jquery < theOptions.jQueryMinVersion) {
				//jQuery is not available load it

				var done = false;
				var script = theWindow.document.createElement("script");
				script.src = "http://ajax.googleapis.com/ajax/libs/jquery/" + theOptions.jQueryDesiredVersion + "/jquery.min.js";
				script.onload = script.onreadystatechange = function () {
					if (!done && (!this.readyState || this.readyState === "loaded" || this.readyState === "complete")) {
						done = true;
						onjQueryLoaded();
					}
				};
				theWindow.document.getElementsByTagName("head")[0].appendChild(script);
			} else {
				onjQueryLoaded();
			}
		}

		//the objects public functions
		return {
			init: function () {

				if (theWindow.location.hostname.match("cliqflip")) {
					alert("Awesome! Your bookmarklet is working.");
					theWindow.hasAnOpenBookmarklet = false;
					return;
				}

				loadjQuery(function () {
					theWindow.hasAnOpenBookmarklet = true;
					createMainContainer();
					var i,
                        images = mediaFinder.getImages(),
                        numOfImages = images.length;

					for (i = 0; i < numOfImages; i++) {
						var imageUrl = images[i];
						createImageStructure(imageUrl).appendTo(contentContainer);
					}
				});
			}
		};
	} ();

	//only open the bookmarklet if it's not open already
	if (!theWindow.hasAnOpenBookmarklet) {
		bookmarklet.init();
	}

})(window, {
	minSize: 75,
	jQueryMinVersion: "1.3.2",
	jQueryDesiredVersion: "1.7.1",
	thumbnailSize: 200,
	endpoint: "http://localhost:51949/popup/bookmarkmedium"
});