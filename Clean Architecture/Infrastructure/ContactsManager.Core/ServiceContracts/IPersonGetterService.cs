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
    public interface IPersonGetterService
    {


        /// <summary>
        /// returns all persons 
        /// </summary>
        /// <returns></returns>
        /// 

        // List<PersonResponse> GetAllPersons();

        Task<PersonResponse> GetPersonByID(Guid? personID);

        Task<List<PersonResponse>> GetAllPersons();

        //SearchBy --> the property
        //SearchString-->the value
        Task<List<PersonResponse>> GetFilteredPersons(string SearchBy, string? SearchString);


    }
}
