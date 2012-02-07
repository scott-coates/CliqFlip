jQuery(function($) {
	var $ = jQuery;


/*------------------------------------*\
  SERVICES BOX
\*------------------------------------*/
  $('.servicebox-hover').css({ opacity: 0 });  
  $('.servicebox-hover').mouseenter(function() {

    $(this).find($('li')).css({ opacity: 0});
    $(this).css({ opacity: 1 });
      $(this).find($('li')).each(function(index) {
        tweenTime = index * 50 + 200;
        delayTime = index * 250;
          $(this).delay(delayTime).animate({opacity: 1}, {queue: false, duration:tweenTime, easing:"easeInQuint"});
        });
  });
  
  $('.servicebox-hover').mouseleave(function() {
    $(this).animate({ opacity: 0 }, {duration:150, easing:"easeOutQuad"});
  });
/*------------------------------------*\
	NAVIGATION
\*------------------------------------*/	
$(" .nav ul ul").css({display: "none"}); // Opera Fix
$(" .nav li").hover(function(){
		$(this).find('ul:first').css({visibility: "visible",display: "none"}).show(400);
		},function(){
		$(this).find('ul:first').css({visibility: "hidden"});
		});

/*------------------------------------*\
	Cycle Slider
\*------------------------------------*/                                                
$(function () {
  $('.cycleslider img:first').fadeIn(1000, function () {
    $('.cycleslider').before('<div class="cycleslider_control_nav">').cycle({
      fx: 'scrollVert, scrollHorz, fade',
      // name of transition effect (or comma separated names, ex: 'fade,scrollUp,shuffle') 
      pause: 1,
      // true to enable "pause on hover" 
      speed: 1000,
      // speed of the transition (any valid fx speed value) 
      timeout: 5000,
      // milliseconds between slide transitions (0 to disable auto advance) 
      randomizeEffects: 1,
      startingSlide: 0,
      easing: 'easeInOutExpo',
      prev: '.cycleslider_previous_control',
      next: '.cycleslider_next_control'
      //pager: '.cycleslider_control_nav'
    });
  });
});		
		
/*------------------------------------*\
	Anything Slider
\*------------------------------------*/	
  // INDEX (ANYTHINGSLIDER)
  if ($('#banner #slides aside').length > 0) {
    $('#banner .dots ul').remove();
    $('#banner #slides > aside').show();
    $('#banner #slides').anythingSlider({
      width: 940,
      //1454// Override the default CSS width
      height: 328,
      // Override the default CSS height
      expand: false,
      // If true, the entire slider will expand to fit the parent element
      resizeContents: false,
      // If true, solitary images/objects in the panel will expand to fit the viewport
      showMultiple: false,
      // Set this value to a number and it will show that many slides at once
      theme: 'default',
      // Theme name - adds a class name to the base element "anythingSlider-{theme}" so the loaded theme will work.
      startPanel: 1,
      // This sets the initial panel
      changeBy: 1,
      // Amount to go forward or back when changing panels.
      hashTags: false,
      // Should links change the hashtag in the URL?
      infiniteSlides: true,
      // if false, the slider will not wrap
      enableKeyboard: false,
      // if false, keyboard arrow keys will not work for the current panel.
      buildArrows: false,
      // If true, builds the forwards and backwards buttons
      buildNavigation: false,
      // If true, builds a list of anchor links to link to each panel
      enableNavigation: true,
      // if false, navigation links will still be visible, but not clickable.
      toggleControls: false,
      // if true, slide in controls (navigation + play/stop button) on hover and slide change, hide @ other times
      appendControlsTo: $('#banner .dots'),
      // A HTML element (jQuery Object, selector or HTMLNode) to which the controls will be appended if not null
      enablePlay: false,
      // if false, the play/stop button will still be visible, but not clickable.
      autoPlay: false,
      // This turns off the entire slideshow FUNCTIONALY, not just if it starts running or not
      autoPlayLocked: false,
      // If true, user changing slides will not stop the slideshow
      startStopped: false,
      // If autoPlay is on, this can force it to start stopped
      pauseOnHover: true,
      // If true & the slideshow is active, the slideshow will pause on hover
      stopAtEnd: false,
      // If true & the slideshow is active, the slideshow will stop on the last page. This also stops the rewind effect when infiniteSlides is false.
      playRtl: false,
      // If true, the slideshow will move right-to-left
      delay: 10000,
      // How long between slideshow transitions in AutoPlay mode (in milliseconds)
      resumeDelay: 1000,
      // Resume slideshow after user interaction, only if autoplayLocked is true (in milliseconds).
      animationTime: 1000,
      // How long the slideshow transition takes (in milliseconds)
      easing: "easeInOutExpo",
      // Anything other than "linear" or "swing" requires the easing plugin
      maxOverallWidth: 32766,
      // Max width (in pixels) of combined sliders (side-to-side); set to 32766 to prevent problems with Opera                  
      onSlideBegin: function (event, slider) {
        if (slider.$targetPage.text().indexOf("welcome-slide") != -1) {
          $('#banner #girl').delay(1000).show().animate({
			left: 300,
            opacity: 1
          }, 1000, "easeOutExpo", function () {
			   $(this).show();
		  });
		  
		  
        } else if (slider.$currentPage.text().indexOf("welcome-slide") != -1) {
          $('#banner #girl').animate({
            left: 600,
            opacity: 0
          }, 1000, "easeOutExpo", function () {
				$(this).hide();
          });
        }
      }
    });
    $('#banner #slides').anythingSliderFx({
      inFx: {
        '.slide1 .left_side h2': {
          left: '0px',
          duration: 800,
          easing: 'easeOutExpo'
        },
        '.slide1 .left_side h3': {
          left: '0px',
          duration: 1200,
          easing: 'easeOutExpo'
        },
		'.slide1 .right_side': {
          top: '0px',
          duration: 800,
          easing: 'easeOutExpo'
        },	
        '.slide2 h2': {
          left: '0px',
          duration: 800,
          easing: 'easeOutExpo'
        },
        '.slide2 h3': {
          left: '0px',
          duration: 1000,
          easing: 'easeOutExpo'
        },
        '.slide2 figure': {
          top: '7px',
          duration: 1200,
          easing: 'easeOutExpo'
        },
        '.slide3 h2': {
          left: '0px',
          duration: 800,
          easing: 'easeOutExpo'
        },
        '.slide3 figure': {
          top: '20px',
          duration: 1000,
          easing: 'easeOutExpo'
        },
        '.slide3 h3': {
          top: '0px',
          duration: 1200,
          easing: 'easeOutExpo'
        }
      },
      outFx: {
        '.slide1 .left_side h2': {
          left: '500px',
          duration: 1200,
          easing: 'easeOutExpo'
        },
        '.slide1 .left_side h3': {
          left: '500px',
          duration: 800,
          easing: 'easeOutExpo'
        },
		'.slide1 .right_side': {
          top: '300px',
          duration: 600,
          easing: 'easeOutExpo'
        },
        '.slide2 h2': {
          left: '500px',
          duration: 1200,
          easing: 'easeOutExpo'
        },
        '.slide2 h3': {
          left: '500px',
          duration: 1000,
          easing: 'easeOutExpo'
        },
        '.slide2 figure': {
          top: '328px',
          duration: 800,
          easing: 'easeOutExpo'
        },
        '.slide3 h2': {
          left: '500px',
          duration: 1200,
          easing: 'easeOutExpo'
        },
        '.slide3 figure': {
          top: '328px',
          duration: 1000,
          easing: 'easeOutExpo'
        },
        '.slide3 h3': {
          top: '328px',
          duration: 800,
          easing: 'easeOutExpo'
        }
      }
    });
    $("#banner .previous a").click(function () {
      $('#banner #slides').data('AnythingSlider').goBack(true);
      return false;
    });
    $("#banner .next a").click(function () {
      $('#banner #slides').data('AnythingSlider').goForward(true);
      return false;
    });
  }
/*------------------------------------*\
	Anything Slider END
\*------------------------------------*/
/*------------------------------------*\
	jQuery UI
\*------------------------------------*/	
	
	// Tabs	
	$(".tabs").tabs();
	
	
	// Toggles	
	$('.toggle-view li').click(function () {
		var text = $(this).children('p');
		
		if (text.is(':hidden')) {
			text.slideDown('fast');
			$(this).children('h4').addClass('active');		
		} else {
			text.slideUp('fast');
			$(this).children('h4').removeClass('active');		
		}		
	});
	
/*------------------------------------*\
	Flickr Feed Start
\*------------------------------------*/
	$('#cbox').jflickrfeed({
		limit: 6, // Number of photos
		qstrings: {
			id: '26947717@N00' // you can find your Flickr id here: http://idgettr.com
		},
		itemTemplate:
		'<li>' +
			'<a rel="colorbox" href="{{image}}" title="{{title}}">' +
				'<img src="{{image_s}}" alt="{{title}}" />' +
			'</a>' +
		'</li>'
	}, function(data) {
		$('#cbox a').colorbox();
	});
/*------------------------------------*\
	Flickr Feed END
\*------------------------------------*/	
/*------------------------------------*\
	Twitter START
\*------------------------------------*/	 	
getTwitters('tweet', { 
  id: 'mariuspop', // Your Twitter ID
  count: 1, 
  enableLinks: true, 
  ignoreReplies: true, 
  clearContents: true,
  template: '<div class="twitter-content"><em>"%text%"</em> </div><div class="date"> <a href="http://twitter.com/%user_screen_name%/statuses/%id_str%/">%time%</a></div>'
});
/*------------------------------------*\
	Twitter END
\*------------------------------------*/	 	
/*------------------------------------*\
	Image hover effect
\*------------------------------------*/
 	
	// Over field	
	$('.over').stop().animate({ "opacity": 0 }, 0);
 	function over() {
		$('.over').hover(function() {
			$(this).stop().animate({ "opacity": .8 }, 250);
		}, function() {
			$(this).stop().animate({ "opacity": 0 }, 250);
		});	
	}	
	over();
	
	// Firefox fix	
	if (window.addEventListener) { 
        window.addEventListener('unload', function() {}, false); 
	} 	
	
	// Opacity change on hover	
	function hover_opacity() {
		$('.portfolio img,.content .gallery, .search_submit').hover(function() {
			$(this).stop().animate({ "opacity": .5 }, 300);
		}, function() {
			$(this).stop().animate({ "opacity": 1 }, 300);
		});
	}	
	hover_opacity();
	
		
/*
 * ---------------------------------------------------------------- 
 *  Quicksand (Sortable Portfolio)
 * ----------------------------------------------------------------  
 */
 
 var items = $('#stage li'),
		itemsByTags = {};
	
	// Looping though all the li items:	
	items.each(function(i){
		var elem = $(this),
			tags = elem.data('tags').split(',');
		
		// Adding a data-id attribute. Required by the Quicksand plugin:
		elem.attr('data-id',i);		
		$.each(tags,function(key,value){
			
			// Removing extra whitespace:
			value = $.trim(value);			
			if(!(value in itemsByTags)){
				// Create an empty array to hold this item:
				itemsByTags[value] = [];
			}			
			// Each item is added to one array per tag:
			itemsByTags[value].push(elem);
		});		
	});
	// Creating the "Everything" option in the menu:
	createList('Everything',items);
	// Looping though the arrays in itemsByTags:
	$.each(itemsByTags,function(k,v){
		createList(k,v);
	});
	
	$('#filter a:first').addClass('active').click();	
	function createList(text,items){	
		// This is a helper function that takes the
		// text of a menu button and array of li items		
		// Creating an empty unordered list:
		var ul = $('<ul>',{'class':'hidden'});	
		$.each(items,function(){
			// Creating a copy of each li item
			// and adding it to the list:			
			$(this).clone().appendTo(ul);
		});
		ul.appendTo('#portfolio_container');
		// Creating a menu item. The unordered list is added
		// as a data parameter (available via .data('list'):		
		var a = $('<a>',{
			html: text,
			href:'#',
			data: {list:ul}
		}).appendTo('#filter');
	}
	
	$('#filter a').live('click',function(e){
		var link = $(this);		
		link.addClass('active').siblings().removeClass('active');	
		
		// Using the Quicksand plugin to animate the li items.
		// It uses data('list') defined by our createList function:			
		$('#stage').quicksand(link.data('list').find('li'),{
			adjustHeight: 'auto'
		}, function() {
			// callback code		
			colorbox();			
			over();
			hover_opacity();
		});		
		e.preventDefault();
	});
	
/*
 * ---------------------------------------------------------------- 
 *  Colorbox
 * ----------------------------------------------------------------  
 */
	function colorbox() {		
		$(".link_colorbox:visible").each(function() {
			var obj = $(this);
			var obj_settings = {};
			if(obj.data("lightbox")){					
				var jObj = jQuery.parseJSON(obj.data("lightbox").toString().replace(/'/g, '"'));
				obj_settings = jObj;
				obj_settings.rel = jObj.group;
			}
			obj.colorbox(obj_settings);
		})

		$(".media_colorbox:visible").colorbox({iframe:true, width:"80%", height:"80%"});		
	}	
	colorbox();		
		
		
/*
 * ---------------------------------------------------------------- 
 *  Skitter Slider
 * ----------------------------------------------------------------  
 */	
	$(function(){
		$('.box_skitter_two_third').skitter({ 
			navigation:false,
			numbers:true,
			animation: "randomSmart",
			label:false
		});
	});
	
		$(function(){
		$('.box_skitter_wide').skitter({ 
			hideTools:true,
			navigation:false,
			numbers:true,
			animation: "cubeJelly",
			label:false
		});
	});
	
/*
 * ---------------------------------------------------------------- 
 *  Contact form Validation
 * ----------------------------------------------------------------  
 */

$("#contact_form").validate();



/*
* ---------------------------------------------------------------- 
*  Login form
* ----------------------------------------------------------------  
*/
    var userLoginContainer = $("#user-login-container");
    $("#user-login").click(function (event) {
        event.preventDefault();
        $(this).toggleClass("selected");
        userLoginContainer.toggle();
    });


    
	
});