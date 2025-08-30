using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        /// <summary>
        /// DTO class for adding a new country
        /// </summary>
        /// 

        //after validating , we have to convert this to a Country obj to add it into the actual data source(List or DB whatever)
        public string? Countryname { get; set; }

        //need of converting the details that the controller sent , theyare just details not an countryobj 
        //because as we said we dont need to expose the model class country , so we just pass these data
        //to the CountryAddRequest DTO and here we convertthese data into a new objectof Country model class

        //method of converting data into a countrymodel class
		public Country ToCountry()
        {
            return new Country() { Countryname = Countryname };
        }
	}


}
