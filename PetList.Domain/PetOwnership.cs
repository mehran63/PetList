using System;
using System.Linq;
using System.Collections.Generic;
using PetList.Entities;
using PetList.Services;
using System.Threading.Tasks;

namespace PetList.Domain
{
    public class PetOwnership : IPetOwnership
    {
        private readonly IPeopleService _peopleService;
        public PetOwnership(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Dictionary<Gender, List<string>>> GetAllCatsOwnerGenderBased()
        {
            var result = new Dictionary<Gender, List<string>>()
            {
                { Gender.Male, new List<string>() },
                { Gender.Female, new List<string>() }
            };

            var personList = await _peopleService.GetAll();

            foreach (var person in personList.Where(p => p.Pets != null))
            {
                foreach (var pet in person.Pets.Where(p => p.Type == PetType.Cat))
                {
                    result[person.Gender].Add(pet.Name);
                }
            }

            result[Gender.Male].Sort();
            result[Gender.Female].Sort();

            return result;

        }
    }
}
