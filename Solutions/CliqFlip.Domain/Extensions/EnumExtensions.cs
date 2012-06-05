using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Enums;
using CliqFlip.Domain.Exceptions;

namespace CliqFlip.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static float ToFloat(this InterestRelationshipWeight weight)
        {
            return ((int) weight)/100.0f;
        }

        public static float GetInterestRelationshipWeight(int level)
        {
            switch (level)
            {
                case 1:
                    return InterestRelationshipWeight.High.ToFloat();
                case 2:
                    return InterestRelationshipWeight.Medium.ToFloat();
                case 3:
                    return InterestRelationshipWeight.Low.ToFloat();
                default:
                    throw new RulesException("Weight", "Invalid level");
            }
        }
    }
}