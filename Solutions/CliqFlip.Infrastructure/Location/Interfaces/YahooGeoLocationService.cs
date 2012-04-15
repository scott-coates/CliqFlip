using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Xml.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Exceptions;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Location.Interfaces
{
	public class YahooGeoLocationService : ILocationService
	{
		private const string _addressFormat = "{0}, {1}, {2}, {3}";
		private const string _mapApiUrl = "http://where.yahooapis.com/geocode?q={0}&appid={1}";
		private static readonly string _appId;
		private readonly IRepository<MajorLocation> _majorLocationRepo;

		static YahooGeoLocationService()
		{
			_appId = ConfigurationManager.AppSettings[Constants.YAHOO_APP_ID];
		}

		public YahooGeoLocationService(IRepository<MajorLocation> majorLocationRepo)
		{
			_majorLocationRepo = majorLocationRepo;
		}

		#region ILocationService Members

		public LocationData GetLocation(string street = null, string city = null, string state = null, string zip = null)
		{
			//TODO: Use the RestSharp library and strongly typed classes
			//TODO: Or use nuget packages for geo - like NGeo
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

		public LocationData GetLocation(string zip)
		{
			var retVal = GetLocation(null, null, null, zip);

			zip = NormalizeZip(zip);

			if(zip != retVal.ZipCode)
			{
				throw new LocationException(zip + " is not a valid zip");
			}

			return retVal;
		}

		private string NormalizeZip(string zip)
		{
			return zip.ToUpperInvariant();
		}

		public LocationData ParseLocationData(string locationData)
		{
			LocationData retVal;

			XElement xdoc = XDocument.Parse(locationData).Root;
			if (xdoc.Element("Error").Value.ToLowerInvariant() != "0")
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
					float.Parse(xdoc.Element("latitude").Value),
					float.Parse(xdoc.Element("longitude").Value),
					null,
					xdoc.Element("statecode").Value,
					xdoc.Element("state").Value,
					xdoc.Element("street").Value,
					xdoc.Element("uzip").Value
					);
			}

			return retVal;
		}

		public MajorLocation GetNearestMajorCity(float latitude, float longitude)
		{
			MajorLocation retVal;
			//Forced to use a manual search because the colons in the query cause an nh exception
			//and a stored proc named query in NH is a pain
			//Not all named parameters have been set: [':Point'

			const string query = @"
					SELECT TOP 1 Id 
					FROM [MajorLocations]
					WHERE [GeoLocation] is not null 
					ORDER BY [GeoLocation].STDistance(GEOGRAPHY::Point({0}, {1}, 4326))";

			var cmdText = String.Format(query, latitude, longitude);
			var sqlConnection = (SqlConnection)NHibernateSession.Current.Connection;

			using (var cmd = new SqlCommand(cmdText, sqlConnection))
			{
				NHibernateSession.Current.Transaction.Enlist(cmd);
				var majorCityId = (int)cmd.ExecuteScalar();
				retVal = _majorLocationRepo.Get(majorCityId);
			}

			return retVal;
		}

		#endregion
	}
}