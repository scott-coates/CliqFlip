function initYouTube(username) {
    //the element that will contain everything
    var container = $("#youtube-container");

    if (container.length == 0) {
        return;
    }

    //create the urls to the users feeds
    var userUrl = "https://gdata.youtube.com/feeds/api/users/" + username,
    playlistFeed = userUrl + "/playlists?v=2",
    uploadsFeed = userUrl + '/uploads?v=2',
    favoritesFeed = userUrl + '/favorites?v=2',

    //create a list with the default playlists. Additional playlists found will be added to this array too.
    playlists = [{ title: "Favorites", url: favoritesFeed, summary: "" },
                    { title: "Uploaded", url: uploadsFeed, summary: ""}];

    //accountInfoContainer - contains the user's youtube information. Subscribe button and playlists
    //playlistInfoContainer - when a playlist is clicked, this container will contain the videos in the playlist just clicked

    var accountInfoContainer = createYouTubeAccountInfoContainer(username).appendTo(container);
    var playlistInfoContainer = createYouTubePlaylistInfoContainer().appendTo(container);    

    //make a call to youtube and retrieve the users playlist feed.
    //The paylist feed has links to all of the users playlists
    //load the the details of each playlist into the accountInfoContainer
    $.getJSON(playlistFeed + "&alt=json&callback=?", function (data) {
        var list_data = "";

        if (data.feed.entry) {
            //iterate through all the entries returned from youtube and
            //add the playlists found to our list
            $.each(data.feed.entry, function (index, item) {
                var playlist = {
                    title: item.title.$t,
                    summary: item.summary.$t,
                    url: item.content.src
                };
                playlists.push(playlist);
            });
        }

        //iterate through all items in the playlist and create the html list
        $.each(playlists, function (index, playlist) {
            list_data += '<li class="youtube-user-item" style="display: block;">' +
                                '<a href="' + playlist.url + '">' +
                                    playlist.title +
                                '</a>' +
                            '</li>';
        });
        $(list_data).appendTo(accountInfoContainer);
    });


    //listen for a click event on any anchors inside an li.youtube-user-item
    //when clicked display all the videos in that playlist
    container.on("click", "li.youtube-user-item a", function (event) {
        event.preventDefault();
        //append parameters so we get JSON and jQuery will recognize use JSONP
        var href = this.href + "&alt=json&callback=?";

        $.getJSON(href, function (data) {
            var list_data = "";
            if (data.feed.entry) {
                //iterate through each of the videos found in the feed
                $.each(data.feed.entry, function (i, item) {

                    //get the title of the video
                    var title = item.title.$t;

                    var url = getVideoUrl(item.link);

                    //get the thumbnail url
                    var thumb = item.media$group.media$thumbnail[0].url;

                    list_data += '<li class="youtube-playlist-item">' +
                                    '<a href="' + url + '" title="' + title + '" target="_blank">' +
                                        '<img alt="' + title + '" src="' + thumb + '"/>' +
                                        '<span>' + title + '</span>' +
                                        //'<span>9 views</span>' +
                                    '</a>' +
                                '</li>';
                });

                $(list_data).appendTo(playlistInfoContainer);
            }
            accountInfoContainer.hide();
            playlistInfoContainer.show();
        });
    });

    //when the back button in the playlistInfoContainer is clicked
    //it should take the user back to the user info screen
    playlistInfoContainer.on("click", "a.back", function (event) {

        //remove all the youtube-playlist-items in case a list was previously loaded
        playlistInfoContainer.find("li.youtube-playlist-item").empty();
        playlistInfoContainer.hide();
        accountInfoContainer.show();
    });
}

function createYouTubeAccountInfoContainer(username) {
    return $("<ul class='youtube-user-info'>" +
                //add an item for the users subscribe button
                "<li>" +
                    "<iframe src='http://www.youtube.com/subscribe_widget?p=" + username + "'" +
                        "style='overflow: hidden; height: 105px; width: 300px; border: 0;'" +
                        "scrolling='no' frameBorder='0'>" +
                    "</iframe>" +
                "</li>" +
            "</ul>");
}

function createYouTubePlaylistInfoContainer() {

    return $("<ul class='youtube-playlist-info'>" +
                "<li>" +
                    "<a class='back'>Back</a>" +
                "</li>" +
            "</ul>");
}

//each entry in a users playlist will have various links associated to it
//the one with a rel attribute of 'alternate' is the url for the user to see
//the video
function getVideoUrl(links) {
    return jQuery.grep(links, function (element, index) {
        return element.rel === 'alternate';
    })[0].href;
}