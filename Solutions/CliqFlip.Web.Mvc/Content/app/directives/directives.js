angular.module('cliqFlipDirectives', []).directive('spinner', function () {
    return function (scope, elm, attrs) {
        var opts = {
            lines: 5, // The number of lines to draw
            length: 7, // The length of each line
            width: 4, // The line thickness
            radius: 10, // The radius of the inner circle
            corners: 1, // Corner roundness (0..1)
            rotate: 66, // The rotation offset
            color: '#000', // #rgb or #rrggbb
            speed: 1, // Rounds per second
            trail: 100, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };
        var target = $(".loading-users",elm[0])[0];
        var spinner = new window.Spinner(opts).spin(target);
    };
});