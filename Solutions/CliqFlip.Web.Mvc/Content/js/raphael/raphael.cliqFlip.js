//http: //raphaeljs.com/ball.html
//http://raphaeljs.com/reference.html#Raphael.fn
//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
Raphael.fn.cliqFlip = {
	createMindMapBubble: function (passion, x, y, fill, text, userInterestId) {
		
		var c = this.circle(x, y, passion * 16).attr({
			fill: fill,
			stroke: "none",
			opacity: .5
		}),
		    s = this.circle(x + (passion * 7), y + passion * 7, 15).attr({
		    	fill: "hsb(.8, .5, .5)",
		    	stroke: "none",
		    	opacity: .5
		    }),
		    t = this.text(x, y, text).attr({
		    	fill: "hsb(.2, .2, .2)",
		    	stroke: "none",
		    	opacity: .5,
		    	"font-size": "15"
		    }).toBack();
		var start = function () {
			// storing original coordinates
			this.ox = this.attr("cx");
			this.oy = this.attr("cy");
			this.or = this.attr('r');
			this.sizer.ox = this.sizer.attr("cx");
			this.sizer.oy = this.sizer.attr("cy");
			this.text.ox = this.text.attr('x');
			this.text.oy = this.text.attr('y');
			this.attr({
				opacity: 1
			});
			this.sizer.attr({
				opacity: 1
			});
			this.animate({ r: this.or2 + 20, opacity: .25 }, 500, ">");
		},
		    move = function (dx, dy) {
		    	var newX = this.ox + dx;
		    	var newY = this.oy + dy;
		    	if (newX >= 0 && newX <= this.node.parentElement.width.baseVal.value && newY >= 0 && newY <= this.node.parentElement.height.baseVal.value) {

		    		// move will be called with dx and dy
		    		this.attr({
		    			cx: newX,
		    			cy: newY
		    		});
		    		this.sizer.attr({
		    			cx: this.sizer.ox + dx,
		    			cy: this.sizer.oy + dy
		    		});
		    		this.text.attr({
		    			x: this.text.ox + dx,
		    			y: this.text.oy + dy
		    		});
		    	}
		    },
		    up = function () {
		    	// restoring state
		    	this.attr({
		    		opacity: .5
		    	});
		    	this.sizer.attr({
		    		opacity: .5
		    	});
		    	this.animate({ r: this.or2, opacity: .5 }, 500, ">");
		    },
		    rstart = function () {
		    	// storing original coordinates
		    	this.ox = this.attr("cx");
		    	this.oy = this.attr("cy");

		    	this.big.or = this.big.attr("r");
		    	this.text.ofs = parseInt(this.text.attr('font-size'), 10);

		    },
		    rmove = function (dx, dy) {
		    	var newR = this.big.or + (dy < 0 ? -1 : 1) * Math.sqrt(2 * dy * dy);
		    	var newFs = this.text.ofs + (dy < 0 ? -1 : 1) * Math.sqrt(2 * (dy / 3) * (dy / 3));

		    	if (newR <= 80 && newR >= 16) {
		    		// move will be called with dx and dy
		    		this.attr({
		    			cx: this.ox + dy,
		    			cy: this.oy + dy
		    		});
		    		this.big.attr({
		    			r: newR
		    		});
		    		this.big.or2 = newR;
		    		this.text.attr({ 'font-size': newFs });
		    	}
		    };
		c.drag(move, start, up);
		c.sizer = s;
		c.text = t;
		//or2 keeps track of the original raidus when
		//the user clicks the bubble and it 'pulses' - prevents the 'fast' double click problem
		c.or2 = c.attr('r'); 
		s.drag(rmove, rstart);
		s.big = c;
		s.text = t;

		return new MindMapBubble(c, s, t, this, userInterestId);
	}
};

function MindMapBubble(big, small, text, paper, userInterestId) {
	this.big = big;
	this.small = small;
	this.text = text;
	this.paper = paper;
	this.userInterestId = userInterestId;
}

MindMapBubble.prototype.GetPassion = function() {
	return this.big.attr('r') / 16;
};