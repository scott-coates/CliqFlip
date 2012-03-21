using System.Collections.Generic;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.Location.Interfaces;
using CliqFlip.Infrastructure.Syndication;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Location
{
	[TestFixture]
	public class LocationServiceTests
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			_locationService = new YahooGeoLocationService();
		}

		#endregion

		private ILocationService _locationService;
		private readonly Dictionary<string, string> _xmlResults = new Dictionary<string, string>();

		public LocationServiceTests()
		{
			#region 92657

			_xmlResults["92657"] =
				@"<?xml version='1.0' encoding='UTF-8'?>
<ResultSet version='1.0'><Error>0</Error><ErrorMessage>No error</ErrorMessage><Locale>us_US</Locale><Quality>40</Quality><Found>1</Found><Result><quality>40</quality><latitude>33.606443</latitude><longitude>-117.828564</longitude><offsetlat>33.606443</offsetlat><offsetlon>-117.828564</offsetlon><radius>5800</radius><name></name><line1></line1><line2>Newport Coast, CA</line2><line3></line3><line4>United States</line4><house></house><street></street><xstreet></xstreet><unittype></unittype><unit></unit><postal></postal><neighborhood></neighborhood><city>Newport Coast</city><county>Orange County</county><state>California</state><country>United States</country><countrycode>US</countrycode><statecode>CA</statecode><countycode></countycode><uzip>92657</uzip><hash></hash><woeid>23417209</woeid><woetype>7</woetype></Result></ResultSet>
<!-- gws23.maps.sp1.yahoo.com uncompressed/chunked Tue Mar 20 20:16:07 PDT 2012 -->
<!-- wws02.geotech.sp2.yahoo.com uncompressed/chunked Tue Mar 20 20:16:07 PDT 2012 -->";

			#endregion

			#region Empty

			_xmlResults["Empty"] =
				@"<?xml version='1.0' encoding='UTF-8'?>
<ResultSet version='1.0'><Error>100</Error><ErrorMessage>No location parameters</ErrorMessage><Locale>us_US</Locale><Quality>0</Quality><Found>0</Found></ResultSet>
<!-- gws16.maps.bf1.yahoo.com uncompressed/chunked Tue Mar 20 20:24:08 PDT 2012 -->
<!-- wws02.geotech.bf1.yahoo.com uncompressed/chunked Tue Mar 20 20:24:08 PDT 2012 -->";

			#endregion

			#region 00000

			_xmlResults["00000"] =
				@"<?xml version='1.0' encoding='UTF-8'?>
<ResultSet version='1.0'><Error>0</Error><ErrorMessage>No error</ErrorMessage><Locale>us_US</Locale><Quality>10</Quality><Found>0</Found></ResultSet>
<!-- gws20.maps.sp1.yahoo.com uncompressed/chunked Tue Mar 20 20:25:32 PDT 2012 -->
<!-- wws03.geotech.sp2.yahoo.com uncompressed/chunked Tue Mar 20 20:25:32 PDT 2012 -->";

			#endregion

		}

		[TestCase("92657", "Newport Coast", "California")]
		[TestCase("Empty",null, null,ExpectedException = typeof(LocationException),ExpectedMessage = "No location parameters")]
		[TestCase("00000", null, null, ExpectedException = typeof(LocationException), ExpectedMessage = "No locations were found")]
		public void LocationServiceFindsCorrectLocation(string zip, string expectedCity, string expectedState)
		{
			LocationData data = _locationService.ParseLocationData(_xmlResults[zip]);
			Assert.That(data.City, Is.EqualTo(expectedCity));
			Assert.That(data.RegionName, Is.EqualTo(expectedState));
		}
	}
}