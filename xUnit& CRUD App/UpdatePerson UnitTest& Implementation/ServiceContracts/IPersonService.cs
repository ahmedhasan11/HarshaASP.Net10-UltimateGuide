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

        //SearchBy --> the property
        //SearchString-->the value
        List<PersonResponse> GetFilteredPersons(string SearchBy, string? SearchString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allPersons">repersents list of persons which will be sorted</param>
        /// <param name="SortBy"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons,string SortBy, SortOrderEnum SortOrder);
        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);



    }
}
