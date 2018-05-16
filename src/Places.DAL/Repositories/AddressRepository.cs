using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Places.DAL.Repositories
{
   public class AddressRepository:IRepository<Address>
    {
        public AddressRepository(DbContext context):base(context)
        {

        }



}
}
