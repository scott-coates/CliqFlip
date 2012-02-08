//http: //raphaeljs.com/ball.html
//http://raphaeljs.com/reference.html#Raphael.fn
//http://stackoverflow.com/questions/3675519/raphaeljs-drag-n-drop
Raphael.fn.cliqFlip = {
	createMindMapBubble: function(passion, x, y, fill, text, userInterestId, canEdit, cb) {
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


		if (canEdit) {
			var afterMoveCb;
			var afterResizeCb;
			var retVal;

			var start = function() {
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
			    move = function(dx, dy) {
			    	var newX = this.ox + dx;
			    	var newY = this.oy + dy;
			    	if (newX >= 0 && newX <= this.node.parentNode.width.baseVal.value && newY >= 0 && newY <= this.node.parentNode.height.baseVal.value) {

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
			    up = function() {
			    	// restoring state
			    	this.attr({
			    		opacity: .5
			    	});
			    	this.sizer.attr({
			    		opacity: .5
			    	});
			    	this.animate({ r: this.or2, opacity: .5 }, 500, ">");
			    	if (cb) {
			    		if (afterMoveCb) {
			    			clearTimeout(afterMoveCb);
			    		}
			    		afterMoveCb = setTimeout(function() { cb(retVal); }, 1000);
			    	}
			    },
			    rstart = function() {
			    	// storing original coordinates
			    	this.ox = this.attr("cx");
			    	this.oy = this.attr("cy");

			    	this.big.or = this.big.attr("r");
			    	this.text.ofs = parseInt(this.text.attr('font-size'), 10);

			    },
			    rmove = function(dx, dy) {
			    	if (this.big.attr('r') == this.big.or2) //make sure they're not double clicking really quickly, then moving the sizer
			    	{
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
			    	}

			    },
			    rup = function() {
			    	if (cb) {
			    		if (afterResizeCb) {
			    			clearTimeout(afterResizeCb);
			    		}
			    		afterResizeCb = setTimeout(function() { cb(retVal); }, 1000);
			    	}
			    };
			c.drag(move, start, up);
			c.sizer = s;
			c.text = t;
			//or2 keeps track of the original raidus when
			//the user clicks the bubble and it 'pulses' - prevents the 'fast' double click problem
			c.or2 = c.attr('r');
			s.drag(rmove, rstart, rup);
			s.big = c;
			s.text = t;
		}

		retVal = new MindMapBubble(c, s, t, this, userInterestId);
		return retVal;
	},
	quotationMarks: function(xAxis) {
		this.path("M" + xAxis + ".505,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.128,0,2.07,0.411,2.826,1.229c0.756,0.82,1.134,1.832,1.134,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.259,0.443-4.327,1.332-6.203c0.888-1.875,2.243-3.57,4.067-5.085c1.824-1.514,2.988-2.272,3.492-2.272c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846zM" + (parseFloat(xAxis) + 13) + ".465,5.873c-3.937,2.52-5.904,5.556-5.904,9.108c0,1.104,0.192,1.656,0.576,1.656l0.396-0.107c0.312-0.12,0.563-0.18,0.756-0.18c1.104,0,2.04,0.411,2.808,1.229c0.769,0.82,1.152,1.832,1.152,3.037c0,1.157-0.408,2.14-1.224,2.947c-0.816,0.807-1.801,1.211-2.952,1.211c-1.608,0-2.935-0.661-3.979-1.984c-1.044-1.321-1.565-2.98-1.565-4.977c0-2.284,0.449-4.369,1.35-6.256c0.9-1.887,2.256-3.577,4.068-5.067c1.812-1.49,2.97-2.236,3.474-2.236c0.336,0,0.612,0.162,0.828,0.486c0.216,0.324,0.324,0.606,0.324,0.846z")
			.attr({ fill: "#000", stroke: "none" });
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