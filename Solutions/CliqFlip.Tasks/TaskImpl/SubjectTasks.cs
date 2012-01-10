using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using Newtonsoft.Json;
using SharpArch.Domain.PersistenceSupport;
using System.Globalization;
using SharpArch.Domain.Specifications;

namespace CliqFlip.Tasks.TaskImpl
{
	public class SubjectTasks : ISubjectTasks
	{
		private readonly IRepository<Subject> _repository;

		public SubjectTasks(IRepository<Subject> repository)
		{
			_repository = repository;
		}

		public string GetSubjectJson()
		{
			var dtos = GetSubjectDtos();
			
			string json = JsonConvert.SerializeObject(dtos);
			
			return json;
		}

		public IList<InterestDto> GetSubjectDtos()
		{
			return _repository.GetAll().Select(x => new InterestDto(x.Id, x.Name)).ToList();
		}

		public void DoSomethingWithInterest()
		{
			throw new System.NotImplementedException();
		}


        public InterestDto SaveOrUpdate(string name)
        {
            var subject = _repository.GetAll().SingleOrDefault(x => x.Name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase));

            if (subject == null)
            {
                //Since the interest does not exist create it.
                var formattedName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
                subject = new Subject(formattedName);

                //TODO: relate the new subject to the know subject

                _repository.SaveOrUpdate(subject);
            }
            return new InterestDto(subject.Id, subject.Name);
        }
    }
}