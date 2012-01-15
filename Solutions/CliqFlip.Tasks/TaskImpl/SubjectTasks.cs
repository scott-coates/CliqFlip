using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
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

		public IList<InterestKeywordDto> GetMatchingKeywords(string input)
		{
			var retVal = new List<InterestKeywordDto>();

			var adHoc = new AdHoc<Subject>(s => s.Name.Contains(input));

			IQueryable<Subject> subjs = _repository.FindAll(adHoc);

			if (subjs.Any())
			{
				retVal.AddRange(subjs.Select(subj => new InterestKeywordDto { Id = subj.Id, SystemAlias = subj.SystemAlias, Name = subj.Name}));
			}
			else
			{
                string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
				retVal.Add(new InterestKeywordDto {Name = formattedName, SystemAlias = "-1" + input.ToLower()});
			}
			return retVal;
		}

		public IList<string> GetSystemAliasAndParentAlias(IList<string> systemAliases)
		{
			var subjectsAndParentsQuery = new AdHoc<Subject>(x => systemAliases.Contains(x.SystemAlias) && x.ParentSubject != null);
			List<string> subjectsAndParents = _repository.FindAll(subjectsAndParentsQuery).Select(x => x.ParentSubject.SystemAlias).ToList();
			subjectsAndParents.AddRange(systemAliases);
			return subjectsAndParents.Distinct().ToList();
		}


		public InterestDto GetOrCreate(string name)
		{
            AdHoc<Subject> withMatchingName = new AdHoc<Subject>(x => x.Name == name);
			Subject subject = _repository.FindOne(withMatchingName);

			if (subject == null)
			{
				//Since the interest does not exist create it.
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
				subject = new Subject(formattedName);

                //TODO: Turn the formatted name into the SystemAlias format
				//TODO: relate the new subject to the know subject

				_repository.Save(subject);
			}
			return new InterestDto(subject.Id, subject.Name, subject.SystemAlias);
		}

		#endregion

		public void DoSomethingWithInterest()
		{
			throw new NotImplementedException();
		}
	}
}