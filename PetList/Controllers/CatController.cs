using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetList.Domain;
using PetList.Entities;

namespace PetList.WebUI.Controllers
{
    [Produces("application/json")]
    public class CatController : Controller
    {
        private readonly IPetOwnership _petOwnership;
        public CatController(IPetOwnership petOwnership)
        {
            _petOwnership = petOwnership;
        }

        [HttpGet]
        [Route("api/cats")]
        [ProducesResponseType(typeof(Dictionary<Gender, List<string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCatsOwnerGenderBased()
        {
            var cats= await _petOwnership.GetAllCatsOwnerGenderBased();
            return Ok(cats);
        }
    }
}