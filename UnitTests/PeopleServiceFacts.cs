using Microsoft.Extensions.Options;
using Moq;
using PetList.Entities;
using PetList.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.eShopOnContainers.BuildingBlocks.Resilience.Http;

namespace UnitTests
{
    public class PeopleServiceFacts
    {
        private readonly Mock<IHttpClient> _httpClient;
        private readonly IOptions<ServiceSettings> _serviceSettings;

        public PeopleServiceFacts()
        {
            _httpClient = new Mock<IHttpClient>();
            _serviceSettings = Options.Create(new ServiceSettings());
        }

        [Fact]
        public async Task GetAll_ReturnsListOfPeople()
        {
            //Arrange
            Person[] expectedResult = new Person[]
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
                }
            };

            _httpClient.Setup(c => c.GetStringAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(@"[{""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""},{""name"":""Fido"",""type"":""Dog""}]}]"));

            //Act
            var peopleService = new PeopleService(_httpClient.Object, _serviceSettings);
            var result = await peopleService.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
