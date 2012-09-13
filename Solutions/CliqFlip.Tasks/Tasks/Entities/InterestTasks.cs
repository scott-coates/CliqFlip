using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.Extensions;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using CliqFlip.Infrastructure.Search;
using Utilities.FileFormats.CSV;

namespace CliqFlip.Tasks.Tasks.Entities
{
    public class InterestTasks : IInterestTasks
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IUserInterestRepository _userInterestRepository;

        public InterestTasks(IInterestRepository interestRepository, IUserInterestRepository userInterestRepository)
        {
            _interestRepository = interestRepository;
            _userInterestRepository = userInterestRepository;
        }

        #region IInterestTasks Members

        public IList<InterestKeywordDto> GetMatchingKeywords(string input)
        {
            var retVal = new List<InterestKeywordDto>();

            var subjs = _interestRepository.GetMatchingKeywords(input).ToList(); //tolist - prevent deferred ex many times

            if (subjs.Any())
            {
                retVal.AddRange(subjs.OrderBy(subj => FuzzySearch.LevenshteinDistance(input, subj.Name)).Take(10).Select(subj => new InterestKeywordDto { Id = subj.Id, Slug = subj.Slug, Name = subj.Name, OriginalInput = input }));
            }

            return retVal;
        }


        public Interest Create(string name, int? relatedTo)
        {
            //TODO: this formatting logic needs to be fixed for some things like iPhone not Iphone
            //and this logic should probably reside in the Interest Class
            string formattedName = name.Trim();
            //string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower().Trim());
            var interest = new Interest(formattedName)
            {
                CreateDate = DateTime.UtcNow,
                ParentInterest = relatedTo.HasValue ? Get(relatedTo.Value) : null
            };

            _interestRepository.SaveOrUpdate(interest);
            return interest;
        }

        public Interest Get(int id)
        {
            return _interestRepository.Get(id);
        }

        public IList<Interest> GetMainCategoryInterests()
        {
            return _interestRepository.GetMainCategoryInterests().ToList();
        }

        public IList<Interest> GetAll()
        {
            return _interestRepository.GetAll();
        }

        public RelatedInterestListDto GetRelatedInterests(string interestSlug)
        {
            return _interestRepository.GetRelatedInterests(interestSlug);
        }

        public void CreateRelationships(RelatedInterestListDto relatedInterestListDto)
        {
            foreach (var newInterest in relatedInterestListDto.WeightedRelatedInterestDtos.Select(x => x.Interest).Where(x => x.Id == 0))
            {
                var createdInterest = Create(newInterest.Name, newInterest.ParentId);
                newInterest.Id = createdInterest.Id;
            }

            if (relatedInterestListDto.WeightedRelatedInterestDtos.Any(x => x.Interest.Id == relatedInterestListDto.OriginalInterest.Id))
            {
                throw new RulesException("Interest", "An interest cannot have a relationship with itself");
            }

            _interestRepository.CreateRelationships(relatedInterestListDto);
        }

        public int UploadInterests(FileStreamDto fileStream)
        {
            int retVal;

            using (var sr = new StreamReader(fileStream.Stream))
            {
                try
                {
                    var csvData = new CSV(sr.ReadToEnd());

                    //skip the first row
                    var interestData = csvData
                        .Rows
                        .Skip(1)
                        .Where(x=> !string.IsNullOrWhiteSpace(x.Cells[0].Value))
                        .Select(
                        x =>
                        {
                            string interestName1 = x[0].Value;
                            
                            if (string.IsNullOrWhiteSpace(interestName1)) throw new RulesException("Interest 1","Interest cannot be missing");

                            var interest = _interestRepository.GetByName(interestName1) ?? Create(interestName1, null);
                            var dto1 = new RelatedInterestListDto.RelatedInterestDto(interest.Id, null, interest.Name, interest.Slug);

                            string interestName2 = x[1].Value;

                            if (string.IsNullOrWhiteSpace(interestName2)) throw new RulesException("Interest 2", "Interest cannot be missing");

                            interest = _interestRepository.GetByName(interestName2) ?? Create(interestName2, null);
                            var dto2 = new RelatedInterestListDto.RelatedInterestDto(interest.Id, null, interest.Name, interest.Slug);

                            return new
                            {
                                Interest1 = dto1,
                                Interest2 = dto2,
                                Weight = int.Parse(x[2].Value)
                            };
                        })
                        .ToList();

                    retVal = interestData.Count;

                    foreach (var data in interestData)
                    {
                        CreateRelationships(
                            new RelatedInterestListDto

                            {
                                OriginalInterest = data.Interest1,
                                WeightedRelatedInterestDtos = new List<RelatedInterestListDto.WeightedRelatedInterestDto>
                                {
                                    new RelatedInterestListDto.WeightedRelatedInterestDto
                                    {
                                        Interest = data.Interest2,
                                        Weight = EnumExtensions.GetInterestRelationshipWeight(data.Weight)
                                    }
                                }
                            });
                    }
                }
                catch (FormatException)
                {
                    throw new RulesException("Weight", "Every row needs to have a proper weight");
                }
            }

            return retVal;
        }

        #endregion
    }
}