using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using Newtonsoft.Json;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Tasks
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
	}
}