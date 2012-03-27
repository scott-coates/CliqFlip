function InitSearchPage() {
    $("div.userSearchBio").dotdotdot();
    $("div.innerUserSearchResults").click(function (event) {
        window.location =
                $(this).find("a.user-profile").attr("href");
    });
}