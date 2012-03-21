using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Exceptions;
using HtmlAgilityPack;

namespace CliqFlip.Infrastructure.Location.Interfaces
{
	public class YahooGeoLocationService : ILocationService
	{
		private const string _addressFormat = "{0}, {1}, {2}, {3}";
		private const string _mapApiUrl = "http://where.yahooapis.com/geocode?q={0}&appid={1}";
		private static readonly string _appId;

		static YahooGeoLocationService()
		{
			_appId = ConfigurationManager.AppSettings[Constants.YAHOO_APP_ID];
		}

		#region ILocationService Members

		public LocationData GetLocation(string street = null, string city = null, string state = null, string zip = null)
		{
			string xmlResult;

			string address = string.Format(_addressFormat, street, city, state, zip);
			string yahooLocationRequest = String.Format(_mapApiUrl, HttpUtility.UrlEncode(address), _appId);

			using (var wc = new WebClient())
			{
				try
				{
					xmlResult = wc.DownloadString(yahooLocationRequest);
				}
				catch (Exception e)
				{
					throw new CriticalException("Error making location request: " + yahooLocationRequest, e);
				}
			}

			return ParseLocationData(xmlResult);

		}

		public LocationData ParseLocationData(string locationData)
		{
			LocationData retVal;

				var xdoc = XDocument.Parse(locationData).Root;
				if(xdoc.Element("Error").Value.ToLowerInvariant() != "0")
				{
					throw new LocationException(xdoc.Element("ErrorMessage").Value);
				}
				else if (xdoc.Element("Found").Value.ToLowerInvariant() == "0")
				{
					throw new LocationException("No locations were found");
				}
				else
				{
					xdoc = xdoc.Element("Result");
					retVal = new LocationData(
						null,
						xdoc.Element("city").Value,
						xdoc.Element("countrycode").Value,
						xdoc.Element("country").Value,
						xdoc.Element("county").Value,
						float.Parse(xdoc.Element("latitude").Value) ,
						float.Parse(xdoc.Element("longitude").Value) ,
						null,
						xdoc.Element("statecode").Value,
						xdoc.Element("state").Value,
						xdoc.Element("street").Value,
						xdoc.Element("uzip").Value
						);
				}
			
			return retVal;
		}

		#endregion
	}
}