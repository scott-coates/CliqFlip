﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CliqFlip.Infrastructure.NHibernateMaps.Overrides
{
	public class ImageMappingOverride: IAutoMappingOverride<Image>
	{
		public void Override(AutoMapping<Image> mapping)
		{
			mapping.Map(x => x.InterestImageOrder).Access.ReadOnly();
		}
	}
}