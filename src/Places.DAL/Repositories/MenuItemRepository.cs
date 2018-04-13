using Places.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{
    class MenuItemRepository:Repository<MenuItem>
    {
        public MenuItemRepository(DbContext context) : base(context)
        {

        }
    }
}
