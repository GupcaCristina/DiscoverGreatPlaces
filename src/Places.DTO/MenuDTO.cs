using System;
using System.Collections.Generic;
using System.Text;

namespace Places.DTO
{
   public class MenuDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  List<MenuItemDTO> MenuItems { get; set; }=new List<MenuItemDTO>();

        
    }
}
