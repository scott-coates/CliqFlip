using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using Newtonsoft.Json;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;

namespace CliqFlip.Tasks.TaskImpl
{
	public class SubjectTasks : ISubjectTasks
	{
		private readonly ILinqRepository<Subject> _repository;

		public SubjectTasks(ILinqRepository<Subject> repository)
		{
			_repository = repository;
		}

		#region ISubjectTasks Members

		public string GetSubjectJson()
		{
			IList<InterestDto> dtos = GetSubjectDtos();

			string json = JsonConvert.SerializeObject(dtos);

			return json;
		}

		public IList<InterestDto> GetSubjectDtos()
		{
			return _repository.FindAll().Select(x => new InterestDto(x.Id, x.Name)).ToList();
		}

		public IList<InterestKeywordDto> GetMatchingKeywords(string input)
		{
			var retVal = new List<InterestKeywordDto>();

			var adHoc = new AdHoc<Subject>(s => s.Name.Contains(input));

			IQueryable<Subject> subjs = _repository.FindAll(adHoc);

			if (subjs.Any())
			{
				retVal.AddRange(subjs.Select(subj => new InterestKeywordDto { SystemAlias = subj.SystemAlias, Name = subj.Name }));
			}
			else
			{
				retVal.Add(new InterestKeywordDto { Name = input, SystemAlias = input.ToLower() });
			}

			return retVal;
		}


		public InterestDto SaveOrUpdate(string name)
		{
			//TODO: Make the db responsible for the matching, not C# code.
			Subject subject = _repository.FindAll().SingleOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

			if (subject == null)
			{
				//Since the interest does not exist create it.
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
				subject = new Subject(formattedName);

				//TODO: relate the new subject to the know subject

				_repository.Save(subject);
			}
			return new InterestDto(subject.Id, subject.Name);
		}

		#endregion

		public void DoSomethingWithInterest()
		{
			throw new NotImplementedException();
		}
	}
}