using Entities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonService
    {
        /// <summary>
        /// adds newpersonto the personslist
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns>returns person details with newlygenerated ID</returns>

        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// returns all persons 
        /// </summary>
        /// <returns></returns>
        /// 

        // List<PersonResponse> GetAllPersons();

        PersonResponse GetPersonByID(Guid? personID);

        List<PersonResponse> GetAllPersons();
    }
}
