﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class ApplicationUser:IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
