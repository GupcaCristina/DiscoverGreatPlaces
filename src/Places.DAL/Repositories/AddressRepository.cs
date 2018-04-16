using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DAL.Repositories
{
   public class AddressRepository:Repository<Address>
    {
        public AddressRepository(DbContext context):base(context)
        {

        }
    }
}
