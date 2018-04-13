using Microsoft.EntityFrameworkCore;
using Places.DTO;

namespace Places.DAL.Repositories
{
    public class MenuRepository:Repository<Menu>
        
    {
        public MenuRepository(DbContext context) : base(context)
        {

        }
    }
}
