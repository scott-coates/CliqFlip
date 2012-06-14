using System;
using System.Device.Location;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class AssignLocationScoreFilter : IAssignLocationScoreFilter
    {
        private const float _metersToMiles = 1609.344f;

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (request == null) throw new ArgumentNullException("request", "Location data is required");
            if (request.LocationData == null) throw new ArgumentNullException("request", "Location data is required");

            var geo = new GeoCoordinate(request.LocationData.Latitude, request.LocationData.Longitude);

            foreach (var user in pipelineResult.Users)
            {
                var distanceTo = (float)geo.GetDistanceTo(new GeoCoordinate(user.Latitude, user.Longitude));

                float miles = distanceTo / _metersToMiles;

                var res = (int)Math.Round(miles, 0, MidpointRounding.AwayFromZero);

                float locationMultiplier = (Constants.LOCATION_MAX_MILES - res) * Constants.LOCATION_MILE_MULTIPLIER;
                if(Math.Abs(locationMultiplier) > float.Epsilon) //float precision - if not 0
                {
                    user.Score *= locationMultiplier;
                }
            }
        }
    }
}