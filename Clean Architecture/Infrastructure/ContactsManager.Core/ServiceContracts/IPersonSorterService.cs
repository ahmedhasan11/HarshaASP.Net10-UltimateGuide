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
    public interface IPersonSorterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allPersons">repersents list of persons which will be sorted</param>
        /// <param name="SortBy"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons,string SortBy, SortOrderEnum SortOrder);



    }
}
