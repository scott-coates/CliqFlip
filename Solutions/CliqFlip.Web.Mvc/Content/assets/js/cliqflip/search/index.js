function InitSearchPage() {
    $("div.userSearchBio").show() //the users bio will start off hidden to prevent flash of overflowing text so show them before applying dotdotdot
                          .dotdotdot();

    $("div.innerUserSearchResults h3").click(function (event) {
        window.location = $(this).find("a.user-profile").attr("href");
    });

    var sliders = $('.interest-slider').anythingSlider(
	{
	    hashTags: false,
	    resizeContents: false,
	    theme: "default1",
	    buildStartStop: false,
	    buildNavigation: false
	});
}