function InitMindMap() {
	var R = Raphael("mindMap", 940, 300),
	    c = R.circle(100, 100, 50).attr({
	    	fill: "hsb(.8, 1, 1)",
	    	stroke: "none",
	    	opacity: .5
	    }),
        s = R.circle(125, 125, 15).attr({
        	fill: "hsb(.8, .5, .5)",
        	stroke: "none",
        	opacity: .5
        }),
        t = R.text(100, 100, "this is text").attr({
        	fill: "hsb(.2, .2, .2)",
        	stroke: "none",
        	opacity: .5,
        	"font-size": "21"
        }).toBack();

        var start = function () {
        	// storing original coordinates
        	this.ox = this.attr("cx");
        	this.oy = this.attr("cy");
        	this.or = this.attr('r');
        	this.sizer.ox = this.sizer.attr("cx");
        	this.sizer.oy = this.sizer.attr("cy")

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
        	// move will be called with dx and dy
        	this.attr({
        		cx: this.ox + dx,
        		cy: this.oy + dy
        	});
        	this.sizer.attr({
        		cx: this.sizer.ox + dx,
        		cy: this.sizer.oy + dy
        	});
        	this.text.attr({
        		x: this.text.ox + dx,
        		y: this.text.oy + dy
        	});
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
        	var newFs = this.text.ofs + (dy < 0 ? -1 : 1) * Math.sqrt(2 * (dy / 2.2) * (dy / 2.2));

        	if (newR <= 130 && newR >= 50) {
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
        c.or2 = c.attr('r');
        s.drag(rmove, rstart);
        s.big = c;
        s.text = t;

}