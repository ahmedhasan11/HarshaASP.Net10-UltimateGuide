using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as areturn type of mostof CountryServices methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? Countryname { get; set; }

		public override bool Equals(object? obj)
		{
            if (obj==null)
            {
                return false;
            }

            if (obj.GetType() != typeof(CountryResponse)){ //if theuser havepasseda differenttype thatis not countryresonse
                return false;
            }

            CountryResponse objresponse = (CountryResponse)obj;

            return CountryID == objresponse.CountryID && Countryname == objresponse.Countryname;

			//return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}

    public static class CountryExtensions {

        //explaining this Country country:


		//هنا كأن Country نفسه عنده ميثود اسمها ToCountryResponse()،
		//لكن الحقيقة هي جاية من الـ extension method اللي انت كتبتها.
		//لاحظ كلمة this Country country 👆
		// معناها: "خلي الميثود دي تبقى شكلها كأنها جزء من كلاس Country"
        //so when you call it you willjust say   countryobj.ToCountryResponse() and assign it into a obj of CountryResponse
		 public static CountryResponse ToCountryResponse(this Country country) //recieves an object of country
         {
            return new CountryResponse() { CountryID = country.CountryID, Countryname = country.Countryname };
         }
    }
}
