using Entities;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonAdderService
    {
        /// <summary>
        /// adds newpersonto the personslist
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns>returns person details with newlygenerated ID</returns>

        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);

    }
}
