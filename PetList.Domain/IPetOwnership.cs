using PetList.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetList.Domain
{
    public interface IPetOwnership
    {
        Task<Dictionary<Gender, List<string>>> GetAllCatsOwnerGenderBased();
    }
}
