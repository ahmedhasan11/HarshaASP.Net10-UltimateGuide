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
    public interface IPersonDeleterService
    {

       Task<bool> DeletePerson(Guid? PersonID); //bool here tell you if deletion is successful or not

    }
}
