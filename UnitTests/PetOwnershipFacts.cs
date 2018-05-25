using FluentAssertions;
using Moq;
using PetList.Domain;
using PetList.Entities;
using PetList.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class PetOwnershipFacts
    {
        private readonly Mock<IPeopleService> _peopleService;

        public PetOwnershipFacts()
        {
            _peopleService = new Mock<IPeopleService>();
        }

        [Fact]
        public async Task GetAllCatsOwnerGenderBased_ReturnsSortedList()
        {
            //Arrange 
            Person[] people = new Person[]
            {
                new Person()
                {
                    Name ="Bob",
                    Gender = Gender.Male,
                    Age = 23,
                    Pets = new Pet[]
                    {
                         new Pet()
                         {
                             Name ="Garfield",
                             Type = PetType.Cat
                         },
                         new Pet()
                         {
                             Name ="Fido",
                             Type = PetType.Dog
                         }
                     }
                },
                new Person()
                {
                    Name ="Kate",
                    Gender = Gender.Female,
                    Age = 23,
                    Pets = new Pet[]
                    {
                         new Pet()
                         {
                             Name ="Tom",
                             Type = PetType.Cat
                         }
                     }
                },
                new Person()
                {
                    Name ="Sam",
                    Gender = Gender.Male,
                    Age = 23,
                    Pets = new Pet[]
                    {
                         new Pet()
                         {
                             Name ="Max",
                             Type = PetType.Cat
                         }
                     }
                }
            };

            Dictionary<Gender, List<string>> expectedResult = new Dictionary<Gender, List<string>>()
            {
                { Gender.Male, new List<string>(){ "Garfield" ,"Max"} },
                { Gender.Female, new List<string>(){ "Tom" } }
            };

            //Act
            _peopleService.Setup(s => s.GetAll()).Returns(Task.FromResult(people));
            var petOwnership = new PetOwnership(_peopleService.Object);
            var result = await petOwnership.GetAllCatsOwnerGenderBased();

            //Act
            Assert.Equal(expectedResult.Count, result.Count);
            Assert.Equal(expectedResult[Gender.Male], result[Gender.Male]);
            Assert.Equal(expectedResult[Gender.Female], result[Gender.Female]);
            //result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
