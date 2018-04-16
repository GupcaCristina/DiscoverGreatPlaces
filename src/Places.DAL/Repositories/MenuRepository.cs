using Microsoft.EntityFrameworkCore;
using Places.Domain;

namespace Places.DAL.Repositories
{
    public class MenuRepository:Repository<Menu>
        
    {
        public MenuRepository(DbContext context) : base(context)
        {

        }
    }
}
