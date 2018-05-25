using PetList.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetList.Services
{
    public interface IPeopleService
    {
        Task<Person[]> GetAll();
    }
}
