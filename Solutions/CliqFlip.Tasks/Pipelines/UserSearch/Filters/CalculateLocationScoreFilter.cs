using System;
using System.Device.Location;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateLocationScoreFilter : ICalculateLocationScoreFilter
    {
        private const float _metersToMiles = 1609.344f;

        public void Filter(UserSearchPipelineResult pipelineResult)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.LocationData == null) throw new ArgumentNullException("pipelineResult", "Location data is required");

            var geo = new GeoCoordinate(pipelineResult.LocationData.Latitude, pipelineResult.LocationData.Longitude);

            foreach (var user in pipelineResult.Users)
            {
                var distanceTo = (float) geo.GetDistanceTo(new GeoCoordinate(user.Latitude, user.Longitude));

                float miles = distanceTo / _metersToMiles;

                var res = (int) Math.Round(miles, 0, MidpointRounding.AwayFromZero);

                float locationMultiplier = res * Constants.LOCATION_MILE_MULTIPLIER;

                user.Score -= locationMultiplier;
            }
        }
    }
}