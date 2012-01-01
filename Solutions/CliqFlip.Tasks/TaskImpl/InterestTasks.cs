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
	public class InterestTasks : IInterestTasks
	{
		private readonly IRepository<Interest> _repository;

		public InterestTasks(IRepository<Interest> repository)
		{
			_repository = repository;
		}

		public string GetInterestJson()
		{
			var dtos = GetInterestDtos();
			
			string json = JsonConvert.SerializeObject(dtos);
			
			return json;
		}

		public IList<InterestDto> GetInterestDtos()
		{
			return _repository.GetAll().Select(x => new InterestDto(x.Id, x.Name)).ToList();
		}

		public void DoSomethingWithInterest()
		{
			throw new System.NotImplementedException();
		}


        public InterestDto GetOrCreate(string name)
        {
            var interest = _repository.GetAll().SingleOrDefault(x => x.Name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase));
            if (interest == null)
            {
                var formattedName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
                interest = new Interest(formattedName);
                _repository.SaveOrUpdate(interest);
            }
            return new InterestDto(interest.Id, interest.Name);
        }
    }
}