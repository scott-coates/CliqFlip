﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Domain.Dtos
{
	public class InterestKeywordDto
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public string OriginalInput { get; set; }
	}
}
