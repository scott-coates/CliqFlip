using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Tasks.Pipelines.UserSearch.Filters;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Pipelines.UserSearch
{
    [TestFixture]
    public class CalculateRelatedInterestScoreFilterTests
    {
        private readonly ICalculateRelatedInterestScoreFilter _calculateRelatedInterestScoreFilter = new CalculateRelatedInterestScoreFilter(null);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInputReturnsError()
        {
            _calculateRelatedInterestScoreFilter.Filter(null, null);
        }

    }
}