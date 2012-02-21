using System.Collections.Generic;
using CliqFlip.Infrastructure.Syndication;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Syndication
{
	[TestFixture]
	public class FeedFinderTests
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			_feedFinder = new FeedFinder();
		}

		#endregion

		private IFeedFinder _feedFinder;
		private readonly Dictionary<string, string> _htmlExamples = new Dictionary<string, string>();

		public FeedFinderTests()
		{
			#region rssOnlyBlog

			_htmlExamples["rssOnly"] =
				@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<!--[if IE 6]>
<html xmlns='http://www.w3.org/1999/xhtml' dir='ltr' lang='en' class='lteIE6'>
<![endif]-->
<!--[if (gt IE 6) | (!IE)]><!-->
<html xmlns='http://www.w3.org/1999/xhtml' dir='ltr' lang='en'>
<!-- <![endif]-->
<head profile='http://gmpg.org/xfn/11'>
<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />

<title> OliveWildly</title>

<!-- Styles  -->
	<link rel='stylesheet' type='text/css' href='http://s1.wp.com/wp-content/themes/pub/modularity-lite/style.css?m=1324583649g' />
	<link rel='stylesheet' href='http://s1.wp.com/wp-content/themes/pub/modularity-lite/library/styles/screen.css?m=1305504205g' type='text/css' media='screen, projection' />
	<link rel='stylesheet' href='http://s1.wp.com/wp-content/themes/pub/modularity-lite/library/styles/print.css?m=1305504205g' type='text/css' media='print' />
	<!--[if lte IE 8]><link rel='stylesheet' href='http://s1.wp.com/wp-content/themes/pub/modularity-lite/library/styles/ie.css?m=1305504205g' type='text/css' media='screen, projection' /><![endif]-->
	<!--[if lte IE 7]><link type='text/css' href='http://s1.wp.com/wp-content/themes/pub/modularity-lite/library/styles/ie-nav.css?m=1305504205g' rel='stylesheet' media='all' /><![endif]-->
<link rel='pingback' href='http://olivewildly.wordpress.com/xmlrpc.php' />


<link rel='alternate' type='application/rss+xml' title='OliveWildly &raquo; Feed' href='http://olivewildly.wordpress.com/feed/' />
<link rel='alternate' type='application/rss+xml' title='OliveWildly &raquo; Comments Feed' href='http://olivewildly.wordpress.com/comments/feed/' />
<script type='text/javascript'>
/* <![CDATA[ */
function addLoadEvent(func){var oldonload=window.onload;if(typeof window.onload!='function'){window.onload=func;}else{window.onload=function(){oldonload();func();}}}
/* ]]> */
</script>
<link rel='stylesheet' href='http://s0.wp.com/wp-content/themes/h4/global.css?m=1313010127g' type='text/css' />
<link rel='stylesheet' id='loggedout-subscribe-css'  href='http://s1.wp.com/wp-content/blog-plugins/loggedout-follow/widget.css?m=1325676615g&#038;ver=20120104' type='text/css' media='all' />
<link rel='stylesheet' id='post-reactions-css'  href='http://s1.wp.com/wp-content/mu-plugins/post-flair/style.css?m=1329504811g&#038;ver=3' type='text/css' media='all' />
<script type='text/javascript' src='http://s1.wp.com/wp-includes/js/jquery/jquery.js?m=1322588689g&#038;ver=1.7.1'></script>
<script type='text/javascript' src='http://s1.wp.com/wp-content/themes/pub/modularity-lite/js/jquery.cycle.js?m=1323834339g&#038;ver=3.4-alpha-19904'></script>
<script type='text/javascript'>
/* <![CDATA[ */
var LoggedOutFollow = {'invalid_email':'Your subscription did not succeed, please try again with a valid email address.'};
/* ]]> */
</script>
<script type='text/javascript' src='http://s1.wp.com/wp-content/blog-plugins/loggedout-follow/widget.js?m=1329327676g&#038;ver=20120215'></script>
<link rel='EditURI' type='application/rsd+xml' title='RSD' href='http://olivewildly.wordpress.com/xmlrpc.php?rsd' />
<link rel='wlwmanifest' type='application/wlwmanifest+xml' href='http://olivewildly.wordpress.com/wp-includes/wlwmanifest.xml' /> 
<meta name='generator' content='WordPress.com' />
<link rel='shortlink' href='http://wp.me/1kvQm' />
<meta property='og:type' content='blog' />
<meta property='og:title' content='OliveWildly' />
<meta property='og:url' content='http://olivewildly.wordpress.com' />
<meta property='og:description' content='' />
<meta property='og:site_name' content='OliveWildly' />
<meta property='og:image' content='' />
<link rel='shortcut icon' type='image/x-icon' href='http://s2.wp.com/i/favicon.ico?m=1311976023g' sizes='16x16 24x24 32x32 48x48' />
<link rel='icon' type='image/x-icon' href='http://s2.wp.com/i/favicon.ico?m=1311976023g' sizes='16x16 24x24 32x32 48x48' />
<link rel='apple-touch-icon-precomposed' href='http://s0.wp.com/i/webclip.png?m=1311618116g' />
<link rel='openid.server' href='http://olivewildly.wordpress.com/?openidserver=1' />
<link rel='openid.delegate' href='http://olivewildly.wordpress.com/' />
<link rel='search' type='application/opensearchdescription+xml' href='http://olivewildly.wordpress.com/osd.xml' title='OliveWildly' />
<link rel='search' type='application/opensearchdescription+xml' href='http://wordpress.com/opensearch.xml' title='WordPress.com' />
<script type='text/javascript' src='http://use.typekit.com/sql0irc.js'></script><script type='text/javascript'>try{Typekit.load();}catch(e){}</script>
	<style type='text/css'>
	/* <![CDATA[ */
			/* ]]> */
	</style>
	
	<script type='text/javascript'>
	/* <![CDATA[ */
		jQuery(document).ready(function(){
			jQuery(function() {
			    jQuery('#slideshow').cycle({
			        speed: '2500',
			        timeout: '500',
					pause: 1
			    });
			});
		});
	/* ]]> */
	</script>

<meta name='application-name' content='OliveWildly' /><meta name='msapplication-window' content='width=device-width;height=device-height' /><meta name='msapplication-task' content='name=Subscribe;action-uri=http://olivewildly.wordpress.com/feed/;icon-uri=http://s2.wp.com/i/favicon.ico' /><meta name='msapplication-task' content='name=Sign up for a free blog;action-uri=http://wordpress.com/signup/;icon-uri=http://s2.wp.com/i/favicon.ico' /><meta name='msapplication-task' content='name=WordPress.com Support;action-uri=http://support.wordpress.com/;icon-uri=http://s2.wp.com/i/favicon.ico' /><meta name='msapplication-task' content='name=WordPress.com Forums;action-uri=http://forums.wordpress.com/;icon-uri=http://s2.wp.com/i/favicon.ico' />
</head>

<body class='home blog typekit-enabled highlander-enabled highlander-dark'>
<div id='top'>

<!-- Begin Masthead -->
<div id='masthead'>
 <h4 class='left'><a href='http://olivewildly.wordpress.com/' title='Home' class='logo'>OliveWildly</a> <span class='description'></span></h4>
</div>

	<div class='main-nav'><ul><li class='current_page_item'><a href='http://olivewildly.wordpress.com/' title='Home'>Home</a></li><li class='page_item page-item-1388'><a href='http://olivewildly.wordpress.com/about-2/'>Fresh&nbsp;Start</a></li><li class='page_item page-item-339'><a href='http://olivewildly.wordpress.com/the-list/'>The&nbsp;List</a></li><li class='page_item page-item-2'><a href='http://olivewildly.wordpress.com/about/'>About</a><ul class='children'><li class='page_item page-item-138'><a href='http://olivewildly.wordpress.com/about/blog-posts/'>Blog&nbsp;Posts</a></li></ul></li></ul></div>

<div class='clear'></div>
</div>

<div class='container'>
<div class='container-inner'>

		<div id='header-image'>
		<img src='http://olivewildly.files.wordpress.com/2011/02/stache4.jpg' width='950' height='200' alt='' />
	</div>
	

	
		<div id='slideshow'>
				<div class='slide'><a href='http://olivewildly.wordpress.com/2012/02/18/restaurant-review-bbq-two22/' rel='bookmark'><img width='950' height='425' src='http://olivewildly.files.wordpress.com/2012/02/bbqtwo20_1.jpg?w=950&amp;h=425&amp;crop=1' class='attachment-modularity-slideshow' alt='BBQTwo20_1' title='BBQTwo20_1' /></a></div>
					<div class='slide'><a href='http://olivewildly.wordpress.com/2012/01/31/camden-image-by-c-m-riordan-2008/' rel='bookmark'><img width='950' height='425' src='http://olivewildly.files.wordpress.com/2012/01/camden1.jpg?w=950&amp;h=425&amp;crop=1' class='attachment-modularity-slideshow' alt='SAMSUNG DIGITAL CAMERA' title='SAMSUNG DIGITAL CAMERA' /></a></div>
			</div><!-- end slideshow -->
	

<div class='span-15 colborder home'>
<h3 class='sub'>Latest</h3>
						<div class='post-2090 post type-post status-publish format-video hentry category-sports tag-video' id='post-2090'>
				<div class='content'>
					<h2><a href='http://olivewildly.wordpress.com/2012/02/19/carving-the-mountains/' rel='bookmark' title='Permanent Link to &#8220;Carving the&nbsp;Mountains&#8221;'>&#8220;Carving the&nbsp;Mountains&#8221;</a></h2>
					<div class='entry'>
						<p>    <iframe src='http://player.vimeo.com/video/24195442' width='590' height='332' frameborder='0' webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe></p>
<p><em>video by Juan Rayos</em></p>
<div class='sharedaddy sharedaddy-dark'></div>											</div>
					<div class='clear'></div>
					<p class='postmetadata alt quiet'>
						February 19, 2012 | Categories: <a href='http://olivewildly.wordpress.com/category/sports/' title='View all posts in Sports' rel='category tag'>Sports</a> | Tags: <a href='http://olivewildly.wordpress.com/tag/video/' rel='tag'>Video</a> | 						<a href='http://olivewildly.wordpress.com/2012/02/19/carving-the-mountains/#respond' title='Comment on &#8220;Carving the&nbsp;Mountains&#8221;'>Leave A Comment &#187;</a>											</p>
				</div>
			</div>
					<div class='post-2078 post type-post status-publish format-standard hentry category-reviews tag-california tag-restaurant' id='post-2078'>
				<div class='content'>
					<h2><a href='http://olivewildly.wordpress.com/2012/02/18/restaurant-review-bbq-two22/' rel='bookmark' title='Permanent Link to Restaurant Review: BBQ Two&nbsp;20'>Restaurant Review: BBQ Two&nbsp;20</a></h2>
					<div class='entry'>
						<p>It doesn&#8217;t take much to get me out to a new place. A beer. Bit of food. All promised at BBQ Two 20.<img class='alignright' src='http://s3-media3.ak.yelpcdn.com/bphoto/uNyVc5C2KAdwlWZw0WJSSA/ms.jpg' alt='' width='100' height='100' /></p>
<p>It&#8217;s a wood-walled, homey joint. The smell of pork and beef was intoxicating for my rumbling stomach. The beer fridge is small but stocked with West Coast beers like Stone Ruination IPA and California Pale Ale (organic). The two cooks running the place are funny and abrupt. Your dad&#8217;s friend that sneaks you a sip of beer and tells hilarious, drunken stories. The music, a mix of 90&#8242;s and 00&#8242;s punk rock, enhances the chill, alt vibe.</p>
<p style='text-align:center;'><a href='http://olivewildly.files.wordpress.com/2012/02/bbqtwo20_1.jpg'><img class='size-full wp-image-2079 aligncenter' title='BBQTwo20_1' src='http://olivewildly.files.wordpress.com/2012/02/bbqtwo20_1.jpg?w=590&#038;h=355' alt='' width='590' height='355' /></a></p>
<p>BBQ Two 20 is located in a rather small building in Costa Mesa, but there are tables both inside and out on the attached patio. Our party of 7 sat comfortably, taking up half the inside real estate while another two groups sat outside. A small family seated beside a group of twenty-somethings and replaced by a gray haired couple. I wouldn&#8217;t expect to see young children or the conservative family among their diners. Between the uncensored music and that feel that it could get raucous with the right catalyst.</p>
<p><a href='http://olivewildly.files.wordpress.com/2012/02/bbqtwo20_2.jpg'><img class='size-medium wp-image-2080 alignright' title='BBQTwo20_2' src='http://olivewildly.files.wordpress.com/2012/02/bbqtwo20_2.jpg?w=258&#038;h=300' alt='' width='258' height='300' /></a>Nearly all the meats are supposedly delicious, according to our friend and BBQ regular. My penchant for pulled pork was only slightly tempted by the other smells, but it won out. I&#8217;m glad it did. While everyone else groaned with delight over brisket and chicken, I was completely taken in by the well done pork. Not stringy, not tough. Really, the meat practically melts, as well as any meat can. There&#8217;s not a lot of sauce or spices to cover for bad grilling. Keeping it to the basics brings out the true, juicy flavors of the meat.</p>
<p>Costs are low for the quality. For two people: 2 sandwiches, 2 local beers   $28.</p>
<p>Hours are 11am &#8211; 9pm everyday</p>
<p>1560 Superior Ave<br />
Costa Mesa, CA 92627<br />
(949) 631-4227<br />
<a title='bbqtwo20.com' href='http://bbqtwo20.com/' target='_blank'>bbqtwo20.com</a></p>
<div class='sharedaddy sharedaddy-dark'></div>											</div>
					<div class='clear'></div>
					<p class='postmetadata alt quiet'>
						February 18, 2012 | Categories: <a href='http://olivewildly.wordpress.com/category/reviews/' title='View all posts in Reviews' rel='category tag'>Reviews</a> | Tags: <a href='http://olivewildly.wordpress.com/tag/california/' rel='tag'>California</a>, <a href='http://olivewildly.wordpress.com/tag/restaurant/' rel='tag'>Restaurant</a> | 						<a href='http://olivewildly.wordpress.com/2012/02/18/restaurant-review-bbq-two22/#respond' title='Comment on Restaurant Review: BBQ Two&nbsp;20'>Leave A Comment &#187;</a>											</p>
				</div>
			</div>
					<div class='post-1233 post type-post status-publish format-image hentry category-travel tag-london' id='post-1233'>
				<div class='content'>
					<h2><a href='http://olivewildly.wordpress.com/2012/01/31/camden-image-by-c-m-riordan-2008/' rel='bookmark' title='Permanent Link to Flashback Snapshot:  Camden&nbsp;2008'>Flashback Snapshot:  Camden&nbsp;2008</a></h2>
					<div class='entry'>
						<p style='text-align:center;'><a href='http://olivewildly.wordpress.com/2012/01/31/camden-image-by-c-m-riordan-2008/'><img class=' wp-image-1216 aligncenter' src='http://olivewildly.files.wordpress.com/2012/01/camden1.jpg?w=600&#038;h=457' alt='Camden - Image by C.M.Riordan, 2008' width='600' height='457' /></a><br />
<em>Camden &#8211; Image by C.M.Riordan, 2008</em></p>
<p>When traveling, I put in quite a bit of effort trying to pass by unnoticed like a local. I&#8217;ve never liked the tourist moniker. It often implies stupidity, gullibility, and straight up in the way. I want to fit in, look like I know where I&#8217;m going, and feel at home. I’ll never really achieve it. It&#8217;s a challenge handling the local look while trying to take it all in. Locals tend to forget to look up and smile at the beauty of the everyday around them. Tourists can&#8217;t help it.</p>
<p>Even though it is a losing battle, I still try. Maybe it harkens back to my high school acting. It’s fun playing pretend. You observe the outstanding characteristics of the locals and emulate, not with your outward appearance, with you body and mindset. A quicker walk, friendly smile (or no smiles), looking like you&#8217;ve all of life on your mind and a destination to get to. It&#8217;s about having purpose. (Or looking like you do).</p>
<p>I believe that we are all many faceted creatures. To different people and situations, we show different parts of ourselves. Trying to blend in where you don’t belong requires you to dig deep and wear some part of yourself that mimics the locale as close as possible. In Camden, I hardly wore the right clothes, hair, or piercings, but I found that rebel part of my soul and embracing it gave me a foundation in that brilliant, alternative world. I understood the need to wear my colors outside of my body; to make a statement about who I was by not looking the “norm”. Embracing that fearless, creative, alternative energy force didn’t change my personality, values, or ideals. Same old me but confident that I belonged. By trying to see the world through locals eyes, I learned more about my own soul. In Camden, it’s not about looking different from everyone else or straying from and idealized norm. It&#8217;s about celebrating yourself as an individual. Taking that creative force inside you and wearing it for all to see. It’s honest.</p>
<div class='sharedaddy sharedaddy-dark'></div>											</div>
					<div class='clear'></div>
					<p class='postmetadata alt quiet'>
						January 31, 2012 | Categories: <a href='http://olivewildly.wordpress.com/category/travel/' title='View all posts in Travel' rel='category tag'>Travel</a> | Tags: <a href='http://olivewildly.wordpress.com/tag/london/' rel='tag'>London</a> | 						<a href='http://olivewildly.wordpress.com/2012/01/31/camden-image-by-c-m-riordan-2008/#respond' title='Comment on Flashback Snapshot:  Camden&nbsp;2008'>Leave A Comment &#187;</a>											</p>
				</div>
			</div>
					<div class='post-1192 post type-post status-publish format-standard hentry category-reviews tag-beauty' id='post-1192'>
				<div class='content'>
					<h2><a href='http://olivewildly.wordpress.com/2012/01/28/ont-bath-bomb/' rel='bookmark' title='Permanent Link to ONT: Bath&nbsp;Bomb'>ONT: Bath&nbsp;Bomb</a></h2>
					<div class='entry'>
						<p><em>One new things is a series of posts I&#8217;m doing with my sister. Each week we challenge each other to try something new and write about it.</em></p>
<p>Not worth it.</p>
<p>For months now I&#8217;ve been dying to take a bath. This is a completely unreasonable desire for me as I A) Dislike being in standing water B) Always feel let down by how quickly water cools off &amp; C) Hate that despite of being half out of water, it&#8217;s very difficult to multitask and do anything else when you&#8217;re wet. All the same, I&#8217;ve dream of luxurious bubbles, steaming water, a good book and a bottle of wine.</p>
<p>So not the experience.</p>
<p>First I couldn&#8217;t get the drain to close in one tub or anything but ice water to run in the other. Then I dropped in the LUSH vanilla bath bomb which fizzed through the scalding water. (I knew it wouldn&#8217;t make bubbles.) Black flecks kept rising to the surface, which I feared was from the facet. Luckily it was just from the vanilla bean. The water smelt heavenly but looked like piss or bad mountain dew. Second, I burned my little toesies over and over trying to get the temp agreeable. Once the bath happened, it was decent. The drain started to go after 5 mins and was nearly gone in another 10. Finally (the cake taker), I had to scrub the tub full on after to rid the walls of the streaks left by the vanilla flecks.</p>
<p>There ends my bathing desire.</p>
<p>Perhaps alcohol would&#8217;ve helped.</p>
<div class='sharedaddy sharedaddy-dark'></div>											</div>
					<div class='clear'></div>
					<p class='postmetadata alt quiet'>
						January 28, 2012 | Categories: <a href='http://olivewildly.wordpress.com/category/reviews/' title='View all posts in Reviews' rel='category tag'>Reviews</a> | Tags: <a href='http://olivewildly.wordpress.com/tag/beauty/' rel='tag'>Beauty</a> | 						<a href='http://olivewildly.wordpress.com/2012/01/28/ont-bath-bomb/#respond' title='Comment on ONT: Bath&nbsp;Bomb'>Leave A Comment &#187;</a>											</p>
				</div>
			</div>
					<div class='post-1191 post type-post status-publish format-standard hentry category-events tag-birthday' id='post-1191'>
				<div class='content'>
					<h2><a href='http://olivewildly.wordpress.com/2012/01/21/impending/' rel='bookmark' title='Permanent Link to Impending'>Impending</a></h2>
					<div class='entry'>
						<p>The birthday is ages (months) away, but I can&#8217;t stop thinking about it. Birthdays are a weird thing for me. Unlike most of my peers, I always looked forward to getting older. Birthdays mean presents, parties, and everyone paying attention to you. I mean what is age really? Just a number that you never feel. I suppose I get that from my mom who loves her birthday. She announces it proudly to anyone and everyone. An anomally among her generation.</p>
<p>That all worked fine until last year. Every year before, I was known for getting my own age wrong. &#8220;How old are you?&#8221; &#8220;25. No, 21. No 24. Heh. sorry.&#8221; I never paid any attention to it. But then I was actually about to turn 25. I don&#8217;t understand it but that seemed weird to me. I&#8217;d always wanted to be a year old than I was, and then&#8230; I was content being 24. But birthdays are unavoidable. I wasn&#8217;t afraid of it or hoping it wouldn&#8217;t happen. It just felt odd. After 24 years of wanting to be older, I didn&#8217;t want to anymore.</p>
<p>I&#8217;ve got 2 months &#8217;til the next big day, and I&#8217;m just as uncomfortable as before. More so.</p>
<p>It can be partly blamed on my employment. Most of my coworkers are in the 21-23 range of heavy drinking and general irresponsibility. My managers are all around my age, but there&#8217;s a rule about fraternizing.</p>
<p>I&#8217;m starting to worry about being too old for things &#8211; something I&#8217;ve never worried before. Strictly speaking, I think everyone could do with acting a bit more childish in the things that count. (and less like tweens in the power plays and bullying that yet pervades adulthood). I&#8217;m following my dream of becoming an author. I really am the happiest I&#8217;ve ever been. So why feel like slinking around behind the birthday and avoiding it all together?</p>
<p>I don&#8217;t want to grow up and do adult things. It&#8217;s not the bill paying. It&#8217;s all the&#8230; expectations. You know, the ones no one (except perhaps my grandma) have for me. Settle down, house with yard, marriage, regular paying work, full savings account&#8230;. Why do these things bug me if no one is pressuring me with them? (ehem, excepting grandma again.) Societal pressures based on media? Watching all my friends settle down? Secret desire? Fear of missing out on the irresponsible things left to do?</p>
<p>Was 26 like this for everyone?</p>
<div class='sharedaddy sharedaddy-dark'></div>											</div>
					<div class='clear'></div>
					<p class='postmetadata alt quiet'>
						January 21, 2012 | Categories: <a href='http://olivewildly.wordpress.com/category/events/' title='View all posts in Events' rel='category tag'>Events</a> | Tags: <a href='http://olivewildly.wordpress.com/tag/birthday/' rel='tag'>Birthday</a> | 						<a href='http://olivewildly.wordpress.com/2012/01/21/impending/#comments' title='Comment on Impending'>1 Comment &#187;</a>											</p>
				</div>
			</div>
		
		<div class='clear'></div>

		<div class='navigation'>
			<div class='alignleft'><a href='http://olivewildly.wordpress.com/page/2/' >&laquo; Older Entries</a></div>
			<div class='alignright'></div>
		</div>

	</div>

<div class='span-8 last'>
	<div id='sidebar'>
	
		<div id='image-8' class='item widget_image'><div style='overflow:hidden;'><img src='http://olivewildly.files.wordpress.com/2012/02/photo.jpg?w=214&amp;h=237' class='alignleft' width='214' height='237' /></div>
</div><div id='categories-4' class='item widget_categories'><h3 class='sub'>Categories</h3>		<ul>
			<li class='cat-item cat-item-18921592'><a href='http://olivewildly.wordpress.com/category/amazeballs/' title='View all posts filed under Amazeballs'>Amazeballs</a>
</li>
	<li class='cat-item cat-item-4804'><a href='http://olivewildly.wordpress.com/category/books-writing/' title='View all posts filed under Books &amp; Writing'>Books &amp; Writing</a>
</li>
	<li class='cat-item cat-item-14560'><a href='http://olivewildly.wordpress.com/category/career/' title='View all posts filed under Career'>Career</a>
<ul class='children'>
	<li class='cat-item cat-item-584057'><a href='http://olivewildly.wordpress.com/category/career/goals-dreams/' title='View all posts filed under Goals &amp; Dreams'>Goals &amp; Dreams</a>
</li>
</ul>
</li>
	<li class='cat-item cat-item-924'><a href='http://olivewildly.wordpress.com/category/events/' title='View all posts filed under Events'>Events</a>
</li>
	<li class='cat-item cat-item-2576'><a href='http://olivewildly.wordpress.com/category/family-friends/' title='View all posts filed under Family &amp; Friends'>Family &amp; Friends</a>
</li>
	<li class='cat-item cat-item-1722'><a href='http://olivewildly.wordpress.com/category/favorites/' title='View all posts filed under Favorites'>Favorites</a>
</li>
	<li class='cat-item cat-item-103'><a href='http://olivewildly.wordpress.com/category/news/' title='View all posts filed under News'>News</a>
</li>
	<li class='cat-item cat-item-2008'><a href='http://olivewildly.wordpress.com/category/projects/' title='View all posts filed under Projects'>Projects</a>
</li>
	<li class='cat-item cat-item-309'><a href='http://olivewildly.wordpress.com/category/reviews/' title='View all posts filed under Reviews'>Reviews</a>
</li>
	<li class='cat-item cat-item-67'><a href='http://olivewildly.wordpress.com/category/sports/' title='View all posts filed under Sports'>Sports</a>
</li>
	<li class='cat-item cat-item-35342200'><a href='http://olivewildly.wordpress.com/category/theories-2/' title='View all posts filed under Theories'>Theories</a>
</li>
	<li class='cat-item cat-item-200'><a href='http://olivewildly.wordpress.com/category/travel/' title='View all posts filed under Travel'>Travel</a>
<ul class='children'>
	<li class='cat-item cat-item-8379691'><a href='http://olivewildly.wordpress.com/category/travel/road-trip-travel/' title='View all posts filed under Road Trip'>Road Trip</a>
</li>
	<li class='cat-item cat-item-139677'><a href='http://olivewildly.wordpress.com/category/travel/study-abroad/' title='View all posts filed under Study Abroad'>Study Abroad</a>
</li>
</ul>
</li>
		</ul>
</div><div id='wp_tag_cloud' class='item wp_widget_tag_cloud'><h3 class='sub'>Tags</h3><div style='overflow:hidden'><a href='http://olivewildly.wordpress.com/tag/accomplishments/' class='tag-link-11836' title='2 topics' style='font-size: 10.625pt;'>Accomplishments</a>
<a href='http://olivewildly.wordpress.com/tag/beauty/' class='tag-link-1885' title='1 topic' style='font-size: 8pt;'>Beauty</a>
<a href='http://olivewildly.wordpress.com/tag/birthday/' class='tag-link-5129' title='5 topics' style='font-size: 15pt;'>Birthday</a>
<a href='http://olivewildly.wordpress.com/tag/build/' class='tag-link-21447' title='1 topic' style='font-size: 8pt;'>Build</a>
<a href='http://olivewildly.wordpress.com/tag/california/' class='tag-link-1337' title='4 topics' style='font-size: 13.833333333333pt;'>California</a>
<a href='http://olivewildly.wordpress.com/tag/canada/' class='tag-link-2443' title='1 topic' style='font-size: 8pt;'>Canada</a>
<a href='http://olivewildly.wordpress.com/tag/career/' class='tag-link-14560' title='2 topics' style='font-size: 10.625pt;'>Career</a>
<a href='http://olivewildly.wordpress.com/tag/christmas/' class='tag-link-15607' title='3 topics' style='font-size: 12.375pt;'>Christmas</a>
<a href='http://olivewildly.wordpress.com/tag/comics/' class='tag-link-756' title='1 topic' style='font-size: 8pt;'>Comics</a>
<a href='http://olivewildly.wordpress.com/tag/concerts/' class='tag-link-1911' title='1 topic' style='font-size: 8pt;'>Concerts</a>
<a href='http://olivewildly.wordpress.com/tag/crosstraining-half-marathon/' class='tag-link-50580052' title='6 topics' style='font-size: 16.020833333333pt;'>Crosstraining</a>
<a href='http://olivewildly.wordpress.com/tag/cycling/' class='tag-link-1676' title='2 topics' style='font-size: 10.625pt;'>Cycling</a>
<a href='http://olivewildly.wordpress.com/tag/dating/' class='tag-link-7313' title='5 topics' style='font-size: 15pt;'>Dating</a>
<a href='http://olivewildly.wordpress.com/tag/diy/' class='tag-link-4315' title='2 topics' style='font-size: 10.625pt;'>DIY</a>
<a href='http://olivewildly.wordpress.com/tag/england/' class='tag-link-1311' title='2 topics' style='font-size: 10.625pt;'>England</a>
<a href='http://olivewildly.wordpress.com/tag/equality/' class='tag-link-47506' title='1 topic' style='font-size: 8pt;'>Equality</a>
<a href='http://olivewildly.wordpress.com/tag/football/' class='tag-link-1134' title='4 topics' style='font-size: 13.833333333333pt;'>Football</a>
<a href='http://olivewildly.wordpress.com/tag/georgia/' class='tag-link-22721' title='1 topic' style='font-size: 8pt;'>Georgia</a>
<a href='http://olivewildly.wordpress.com/tag/half-marathon/' class='tag-link-43722' title='3 topics' style='font-size: 12.375pt;'>Half Marathon</a>
<a href='http://olivewildly.wordpress.com/tag/health/' class='tag-link-337' title='1 topic' style='font-size: 8pt;'>Health</a>
<a href='http://olivewildly.wordpress.com/tag/ireland/' class='tag-link-768' title='2 topics' style='font-size: 10.625pt;'>Ireland</a>
<a href='http://olivewildly.wordpress.com/tag/kentucky/' class='tag-link-59349' title='1 topic' style='font-size: 8pt;'>Kentucky</a>
<a href='http://olivewildly.wordpress.com/tag/kitchen-adventures/' class='tag-link-67496' title='3 topics' style='font-size: 12.375pt;'>Kitchen Adventures</a>
<a href='http://olivewildly.wordpress.com/tag/knitting/' class='tag-link-1336' title='1 topic' style='font-size: 8pt;'>Knitting</a>
<a href='http://olivewildly.wordpress.com/tag/london/' class='tag-link-1618' title='2 topics' style='font-size: 10.625pt;'>London</a>
<a href='http://olivewildly.wordpress.com/tag/memories/' class='tag-link-3869' title='3 topics' style='font-size: 12.375pt;'>Memories</a>
<a href='http://olivewildly.wordpress.com/tag/michigan/' class='tag-link-11110' title='1 topic' style='font-size: 8pt;'>Michigan</a>
<a href='http://olivewildly.wordpress.com/tag/music/' class='tag-link-18' title='6 topics' style='font-size: 16.020833333333pt;'>Music</a>
<a href='http://olivewildly.wordpress.com/tag/nevada/' class='tag-link-59354' title='2 topics' style='font-size: 10.625pt;'>Nevada</a>
<a href='http://olivewildly.wordpress.com/tag/new-york/' class='tag-link-4614' title='1 topic' style='font-size: 8pt;'>New York</a>
<a href='http://olivewildly.wordpress.com/tag/north-carolina/' class='tag-link-20037' title='3 topics' style='font-size: 12.375pt;'>North Carolina</a>
<a href='http://olivewildly.wordpress.com/tag/pennsylvania/' class='tag-link-11113' title='3 topics' style='font-size: 12.375pt;'>Pennsylvania</a>
<a href='http://olivewildly.wordpress.com/tag/planning/' class='tag-link-4599' title='1 topic' style='font-size: 8pt;'>Planning</a>
<a href='http://olivewildly.wordpress.com/tag/politics/' class='tag-link-398' title='5 topics' style='font-size: 15pt;'>Politics</a>
<a href='http://olivewildly.wordpress.com/tag/roller-derby/' class='tag-link-105281' title='17 topics' style='font-size: 22pt;'>Roller Derby</a>
<a href='http://olivewildly.wordpress.com/tag/rugby/' class='tag-link-6100' title='1 topic' style='font-size: 8pt;'>Rugby</a>
<a href='http://olivewildly.wordpress.com/tag/running/' class='tag-link-1675' title='1 topic' style='font-size: 8pt;'>Running</a>
<a href='http://olivewildly.wordpress.com/tag/south-carolina/' class='tag-link-11112' title='1 topic' style='font-size: 8pt;'>South Carolina</a>
<a href='http://olivewildly.wordpress.com/tag/surfing/' class='tag-link-822' title='1 topic' style='font-size: 8pt;'>Surfing</a>
<a href='http://olivewildly.wordpress.com/tag/theories-2/' class='tag-link-35342200' title='3 topics' style='font-size: 12.375pt;'>Theories</a>
<a href='http://olivewildly.wordpress.com/tag/valentines-day/' class='tag-link-13766' title='3 topics' style='font-size: 12.375pt;'>Valentine's Day</a>
<a href='http://olivewildly.wordpress.com/tag/video/' class='tag-link-412' title='16 topics' style='font-size: 21.5625pt;'>Video</a>
<a href='http://olivewildly.wordpress.com/tag/wisconsin/' class='tag-link-11108' title='11 topics' style='font-size: 19.375pt;'>Wisconsin</a>
<a href='http://olivewildly.wordpress.com/tag/wishes/' class='tag-link-1541' title='4 topics' style='font-size: 13.833333333333pt;'>Wishes</a>
<a href='http://olivewildly.wordpress.com/tag/woodworking/' class='tag-link-44926' title='2 topics' style='font-size: 10.625pt;'>Woodworking</a></div></div><div id='archives-3' class='item widget_archive'><h3 class='sub'>Archives</h3>		<ul>
			<li><a href='http://olivewildly.wordpress.com/2012/02/' title='February 2012'>February 2012</a></li>
	<li><a href='http://olivewildly.wordpress.com/2012/01/' title='January 2012'>January 2012</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/11/' title='November 2011'>November 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/10/' title='October 2011'>October 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/08/' title='August 2011'>August 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/07/' title='July 2011'>July 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/05/' title='May 2011'>May 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/04/' title='April 2011'>April 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/03/' title='March 2011'>March 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/02/' title='February 2011'>February 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2011/01/' title='January 2011'>January 2011</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/12/' title='December 2010'>December 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/11/' title='November 2010'>November 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/10/' title='October 2010'>October 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/09/' title='September 2010'>September 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/08/' title='August 2010'>August 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/07/' title='July 2010'>July 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/06/' title='June 2010'>June 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/05/' title='May 2010'>May 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/04/' title='April 2010'>April 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2010/02/' title='February 2010'>February 2010</a></li>
	<li><a href='http://olivewildly.wordpress.com/2009/12/' title='December 2009'>December 2009</a></li>
	<li><a href='http://olivewildly.wordpress.com/2009/11/' title='November 2009'>November 2009</a></li>
	<li><a href='http://olivewildly.wordpress.com/2009/09/' title='September 2009'>September 2009</a></li>
	<li><a href='http://olivewildly.wordpress.com/2009/08/' title='August 2009'>August 2009</a></li>
	<li><a href='http://olivewildly.wordpress.com/2009/07/' title='July 2009'>July 2009</a></li>
	<li><a href='http://olivewildly.wordpress.com/2006/01/' title='January 2006'>January 2006</a></li>
		</ul>
</div>
	</div>
</div>
</div>
<div class='double-border'></div>
<div class='clear'></div>
</div>
</div>
<div id='footer-wrap'>
<div id='footer'>
<div class='span-6 small'>
	</div>
<div class='column span-6 small'>
	<div id='text-3' class='item widget_text'>			<div class='textwidget'><p>'The fact that we live at the bottom of a deep gravity well, on the surface of a gas covered planet going around a nuclear fireball 90 million miles away and think this to be normal is obviously some indication of how skewed our perspective tends to be.'<br />
— Douglas Adams </p>
</div>
		</div></div>
<div class='column span-6 small'>
	<div id='image-7' class='item widget_image'><h3 class='sub'>On my wish list:</h3><div style='overflow:hidden;'><a href='http://www.atomwheels.com/Quad/pulse.html'><img src='http://www.atomwheels.com/Quad/images/wheels/pulse-sm.jpg' title='Atom Quad Wheels: Pulse' class='alignleft' width='225' height='200' /></a></div>
</div></div>
<div class='column span-6 small last'>
	<div id='image-5' class='item widget_image'><h3 class='sub'>[stuck] In my head:</h3><div style='overflow:hidden;'><a href='http://www.last.fm/music/Smoking+Popes/_/I+Know+You+Love+Me'><img src='http://t1.gstatic.com/images?q=tbn:ANd9GcQN10rsEryU_UMoU0cbDevNNJB3inlHoka89yxXDmRPRXZqA_b3' title='Smoking Popes - I Know You Love Me' class='alignleft' width='150' height='200' /></a></div>
</div></div>
<div class='clear'></div>
<p class='small quiet'><a href='http://wordpress.com/?ref=footer' rel='generator'>Blog at WordPress.com</a>. | Theme: <a href='http://theme.wordpress.com/themes/modularity-lite/'>Modularity Lite</a> by <a href='http://graphpaperpress.com/' rel='designer'>Graph Paper Press</a>. <a href='http://en.support.wordpress.com/custom-design/#custom-fonts' rel='external' title='Learn more about the fonts on this blog and the Custom Design upgrade'>Fonts on this blog</a>.</p>
</div>
</div>
<script type='text/javascript' src='http://s.gravatar.com/js/gprofiles.js?aa&#038;ver=3.4-alpha-19904'></script>
<script type='text/javascript'>
/* <![CDATA[ */
var WPGroHo = {'my_hash':''};
/* ]]> */
</script>
<script type='text/javascript' src='http://s1.wp.com/wp-content/mu-plugins/gravatar-hovercards/wpgroho.js?m=1318621575g&#038;ver=3.4-alpha-19904'></script>

<script type='text/javascript'>
var _qevents = _qevents || [], wpcomQuantcastData = {'qacct':'p-18-mFEk4J448M','labels':',language.en,type.wpcom'};
function wpcomQuantcastPixel( labels, options ) {
	var i, defaults = wpcomQuantcastData, data = { event: 'ajax' };

	labels  = labels  || '';
	options = options || {};

	if ( typeof labels != 'string' )
		options = labels;

	for ( i in defaults ) {
		data[i] = defaults[i];
	}

	for ( i in options ) {
		data[i] = options[i];
	}

	if ( data.labels ) {
		data.labels += ',' + labels;
	} else {
		data.labels = labels;
	}

	_qevents.push( data );
};
(function() {var elem = document.createElement('script');elem.src = (document.location.protocol == 'https:' ? 'https://secure' : 'http://edge') + '.quantserve.com/quant.js';elem.async = true;elem.type = 'text/javascript';var scpt = document.getElementsByTagName('script')[0];scpt.parentNode.insertBefore(elem, scpt);  })();
_qevents.push( wpcomQuantcastData );
</script>
<noscript><div style='display: none;'><img src='//pixel.quantserve.com/pixel/p-18-mFEk4J448M.gif?labels=%2Clanguage.en%2Ctype.wpcom' height='1' width='1' alt='' /></div></noscript>

<script>jQuery(document).ready(function($){ Gravatar.profile_cb = function( h, d ) { WPGroHo.syncProfileData( h, d );	}; Gravatar.my_hash = WPGroHo.my_hash; Gravatar.init( 'body', '#wp-admin-bar-my-account' ); });</script>	<div style='display:none'>
	</div>
		<style type='text/css'>
			
				.reblog-from img                   { margin: 0 10px 0 0; vertical-align: middle; padding: 0; border: 0; }
				.reblogger-note img.avatar         { float: left; padding: 0; border: 0; }
				.reblogger-note-content            { margin: 0 0 20px 35px; }
				.reblog-post                       { border-left: 3px solid #eee; padding-left: 15px; }
				.reblog-post ul.thumb-list         { display: block; list-style: none; margin: 2px 0; padding: 0; clear: both; }
				.reblog-post ul.thumb-list li      { display: inline; margin: 0; padding: 0 1px; border: 0; }
				.reblog-post ul.thumb-list li a    { margin: 0; padding: 0; border: 0; }
				.reblog-post ul.thumb-list li img  { margin: 0; padding: 0; border: 0; }
				.reblog-post                       { border-left: 3px solid #eee; padding-left: 15px; }
					</style>	
	<div id='bit' class='loggedout-follow-typekit'>
		<a class='bsub' href='javascript:void(0)'><span id='bsub-text'>Follow</span></a>
		<div id='bitsubscribe'>
			
					<h3><label for='loggedout-follow-field'>Follow &ldquo;OliveWildly&rdquo;</label></h3>
		
			<form action='https://subscribe.wordpress.com' method='post' accept-charset='utf-8' id='loggedout-follow'>
			<p>Get every new post delivered to your Inbox.</p>
			
			<p id='loggedout-follow-error' style='display: none;'></p>

					
			
			<p><input type='text' name='email' style='width: 95%; padding: 1px 2px' value='Enter your email address' onfocus='this.value=(this.value=='Enter your email address') ? '' : this.value;' onblur='this.value=(this.value=='') ? 'Enter email address' : this.value;'  id='loggedout-follow-field'/></p>

			<input type='hidden' name='action' value='subscribe'/>
			<input type='hidden' name='blog_id' value='19665306'/>
			<input type='hidden' name='source' value='http://olivewildly.wordpress.com/'/>
			<input type='hidden' name='sub-type' value='loggedout-follow'/>

			<input type='hidden' id='_wpnonce' name='_wpnonce' value='c949d1fcb3' /><input type='hidden' name='_wp_http_referer' value='/' />
			<p id='bsub-subscribe-button'><input type='submit' value='Sign me up' /></p>
			</form>
					<div id='bsub-credit'><a href='http://wordpress.com/signup/?ref=lof'>Powered by WordPress.com</a></div>
		</div><!-- #bitsubscribe -->
	</div><!-- #bit -->
<script type='text/javascript'>
// <![CDATA[
(function() {
try{
  if ( window.external &&'msIsSiteMode' in window.external) {
    if (window.external.msIsSiteMode()) {
      var jl = document.createElement('script');
      jl.type='text/javascript';
      jl.async=true;
      jl.src='/wp-content/plugins/ie-sitemode/custom-jumplist.php';
      var s = document.getElementsByTagName('script')[0];
      s.parentNode.insertBefore(jl, s);
    }
  }
}catch(e){}
})();
// ]]>
</script><script src='http://s.stats.wordpress.com/w.js?21' type='text/javascript'></script>
<script type='text/javascript'>
st_go({'blog':'19665306','v':'wpcom','user_id':'0','subd':'olivewildly'});
ex_go({'crypt':'UE40eW5QN0p8M2Y/RE1LVmwrVi5vQS5fVFtfdHBbPyw1VXIrU3hWLHhzVndTdktBX0ddJnpXRjVaOTd6fj1YMX4ydzRCalRHfjMsJUJlVGk4NXB6V09scmIyNlFXLTJ8c0YyVUpYN0ZyMVQxSzZ+eHdkYjFwY0Yzfi1WPXVqbmJTLFg0QkQ0WyVacV9BNix8RGc4RGRnbHZ0fHplWy1KV0U/a2kvTyx8aDI1SDImUTF6P34uZ3dwaHBxcGtPdDBpXV1uM2JiSUF+aGVQRWhPaFR3dklRVXBFJVpsbywxMiVPLTR5LCZNfHY3ZE1seG51YllOSStdTlUrVEdEdmhCQ1pPVnM0elFhOA=='});
addLoadEvent(function(){linktracker_init('19665306',0);});
	</script>
<noscript><img src='http://stats.wordpress.com/b.gif?v=noscript' style='height:0px;width:0px;overflow:hidden' alt='' /></noscript>
</body>
</html>";

			#endregion

			#region noFeed

			_htmlExamples["noFeed"] =
				@"<!DOCTYPE html>
<html dir='ltr' lang='en-US'>
<head>
<meta charset='UTF-8' />
<title>HiScotty - Top of the line .NET developer out of Newport Coast, CA.Hi Scotty | &#8230;Scott Who?</title>
<link rel='profile' href='http://gmpg.org/xfn/11' />
<link rel='stylesheet' type='text/css' media='all' href='http://hiscotty.com/wordpress/wp-content/themes/twentyten/style.css' />
<link rel='pingback' href='http://hiscotty.com/wordpress/xmlrpc.php' />

<!-- This site is optimized with the Yoast WordPress SEO plugin v1.0.3 - http://yoast.com/wordpress/seo/ -->
<meta name='description' content='Professionally personal website for Scott Coates, a .NET programmer from Newport Coast, CA.'/>
<link rel='canonical' href='http://hiscotty.com/' />
<meta name='google-site-verification' content='JqzmUHE1HRLt7BRTGHxR09xY5nHiEi7l7OS_ar6MJX0' />
<!-- / Yoast WordPress SEO plugin. -->
	<script type='text/javascript'>//<![CDATA[
	// Google Analytics for WordPress by Yoast v4.1.3 | http://yoast.com/wordpress/google-analytics/
	var _gaq = _gaq || [];
	_gaq.push(['_setAccount','UA-20810961-1']);
	_gaq.push(['_trackPageview'],['_trackPageLoadTime']);
	(function() {
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	})();
	//]]></script>
<script type='text/javascript' src='http://hiscotty.com/wordpress/wp-includes/js/l10n.js?ver=20101110'></script>
<script type='text/javascript' src='http://hiscotty.com/wordpress/wp-includes/js/comment-reply.js?ver=20090102'></script>
<script type='text/javascript' src='http://hiscotty.com/wordpress/wp-includes/js/jquery/jquery.js?ver=1.6.1'></script>
<link rel='EditURI' type='application/rsd+xml' title='RSD' href='http://hiscotty.com/wordpress/xmlrpc.php?rsd' />
<link rel='wlwmanifest' type='application/wlwmanifest+xml' href='http://hiscotty.com/wordpress/wp-includes/wlwmanifest.xml' /> 
<meta name='generator' content='WordPress 3.2.1' />
<script type='text/javascript'>AC_FL_RunContent = 0;</script><script type='text/javascript' src='http://hiscotty.com/wordpress/wp-content/plugins/dynamic-headers/AC_RunActiveContent.js'></script><link rel='stylesheet' type='text/css' href='http://hiscotty.com/wordpress/wp-content/plugins/social-media-widget/social_widget.css' />
<!-- BEGIN Hackadelic Sliding Notes 1.6.5 -->
<style type='text/css'>
.concealed { display: none }
.block { display: block }
</style>
<!-- END Hackadelic Sliding Notes 1.6.5 -->
</head>

<body class='home page page-id-2 page-template-default'>
<div id='wrapper' class='hfeed'>
	<div id='header'>
		<div id='masthead'>
			<div id='branding' role='banner'>
								<h1 id='site-title'>
					<span>
						<a href='http://hiscotty.com/' title='Hi Scotty' rel='home'>Hi Scotty</a>
					</span>
				</h1>
				<div id='site-description'>&#8230;Scott Who?</div>

										<img src='http://hiscotty.com/wordpress/wp-content/uploads/2011/01/Header.png' width='940' height='198' alt='' />
								</div><!-- #branding -->

			<div id='access' role='navigation'>
			  				<div class='skip-link screen-reader-text'><a href='#content' title='Skip to content'>Skip to content</a></div>
								<div class='menu-header'><ul id='menu-home' class='menu'><li id='menu-item-40' class='menu-item menu-item-type-post_type menu-item-object-page current-menu-item page_item page-item-2 current_page_item menu-item-40'><a href='http://hiscotty.com/'>I&#8217;m Scott</a></li>
<li id='menu-item-108' class='menu-item menu-item-type-custom menu-item-object-custom menu-item-108'><a title='LinkedIn Résumé' href='/resume'>Résumé</a></li>
<li id='menu-item-42' class='menu-item menu-item-type-custom menu-item-object-custom menu-item-42'><a title='Need a smart programmer who gets things done?' href='/CV'>StackOverflow CV</a></li>
<li id='menu-item-51' class='menu-item menu-item-type-post_type menu-item-object-page menu-item-51'><a href='http://hiscotty.com/what-i-look-for-in-a-great-company/'>What I Look for in a Great Company</a></li>
<li id='menu-item-90' class='menu-item menu-item-type-post_type menu-item-object-page menu-item-90'><a href='http://hiscotty.com/contact/'>Contact</a></li>
</ul></div>			</div><!-- #access -->
		</div><!-- #masthead -->
	</div><!-- #header -->

	<div id='main'>

		<div id='container'>
			<div id='content' role='main'>

			

				<div id='post-2' class='post-2 page type-page status-publish hentry'>
											<h2 class='entry-title'>I&#8217;m Scott</h2>
					
					<div class='entry-content'>
						<p>I am a geek.</p>
<p>A self-motivated geek.</p>
<p>A self-motivated geek who lives by the old adage of &#8216;Work Smart. Not Hard&#8217;&#8230;</p>
<p>&#8230;And some other clichés.</p>
<hr />
<p>I am a .NET developer from Newport Coast, CA.  I like working with companies which are forward-thinking and challenging.  The following lists some examples of technologies I enjoy using:</p>
<ul>
<li><a href='http://www.asp.net/mvc' onclick='javascript:_gaq.push(['_trackEvent','outbound-article','http://www.asp.net']);' target='_blank'>ASP.NET MVC</a></li>
<li><a href='http://www.nunit.org/' onclick='javascript:_gaq.push(['_trackEvent','outbound-article','http://www.nunit.org']);' target='_blank'>NUnit</a></li>
<li><a href='https://www.packtpub.com/nhibernate-3-0-cookbook/book' onclick='javascript:_gaq.push(['_trackEvent','outbound-article','http://www.packtpub.com']);' target='_blank'>NHibernate</a></li>
<li><a href='http://www.fogcreek.com/' onclick='javascript:_gaq.push(['_trackEvent','outbound-article','http://www.fogcreek.com']);' target='_blank'>FogBugz/Kiln</a></li>
</ul>
<p>You can check out my qualifications by referring to my <a href='http://hiscotty.com/resume' >résumé</a> or my <a href='http://hiscotty.com/CV' >CV</a> for an extended introduction.</p>
																	</div><!-- .entry-content -->
				</div><!-- #post-## -->

				
			<div id='comments'>


	<p class='nocomments'>Comments are closed.</p>


								
</div><!-- #comments -->


			</div><!-- #content -->
		</div><!-- #container -->


		<div id='primary' class='widget-area' role='complementary'>
			<ul class='xoxo'>

<li id='social-widget-3' class='widget-container Social_Widget'><h3 class='widget-title'>Swing by and say hi</h3><div class='socialmedia-buttons smw_left'><p>If you don't, there's a good chance I might develop a stutter. Puh-puh-puh-please don't do this to me.</p>
<a href='/resume'  ><img  src='http://hiscotty.com/wordpress/wp-content/plugins/social-media-widget/images/default/64/linkedin.png' alt='Follow me on LinkedIn' title='Follow me on LinkedIn'  style='opacity: 0.8; -moz-opacity: 0.8;' class='fade' /></a></div></li>			</ul>
		</div><!-- #primary .widget-area -->


		<div id='secondary' class='widget-area' role='complementary'>
			<ul class='xoxo'>
				<li id='text-3' class='widget-container widget_text'>			<div class='textwidget'><a href='http://hiscotty.com/CV'>
<img src='http://careers.stackoverflow.com/Content/Img/logo-careers-2-so.png?31e238' width='208' height='58' alt='Need a smart programmer who gets things done?' title='Need a smart programmer who gets things done?'>
</a>
<a href='http://stackoverflow.com/users/173957/scoarescoare'>
<img src='http://stackoverflow.com/users/flair/173957.png?theme=dark' width='208' height='58' alt='Stack Overflow profile for scoarescoare at Stack Overflow, Q&amp;A for professional and enthusiast programmers' title='Stack Overflow profile for scoarescoare at Stack Overflow, Q&amp;A for professional and enthusiast programmers'>
</a>
</div>
		</li>			</ul>
		</div><!-- #secondary .widget-area -->

	</div><!-- #main -->

	<div id='footer' role='contentinfo'>
		<div id='colophon'>



			<div id='site-info'>
				<a href='http://hiscotty.com/' title='Hi Scotty' rel='home'>
					Hi Scotty				</a>
			</div><!-- #site-info -->

			<div id='site-generator'>
								<a href='http://wordpress.org/' title='Semantic Personal Publishing Platform' rel='generator'>Proudly powered by WordPress.</a>
			</div><!-- #site-generator -->

		</div><!-- #colophon -->
	</div><!-- #footer -->

</div><!-- #wrapper -->

</body>
</html>
";

			#endregion

		}

		[TestCase("rssOnly", "http://olivewildly.wordpress.com/feed/")]
		[TestCase("noFeed", null)]
		public void FeedFinderFindsFeed(string html, string expectedFeedUrl)
		{
			var actualFeedLink = _feedFinder.GetFeedUrl(_htmlExamples[html]);
			Assert.That(actualFeedLink, Is.EqualTo(expectedFeedUrl));
		}
	}
}