using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
   public class Menu :Entity<int>
    {
        public string Name { get; set; }
        public  List<MenuItem> MenuItems { get; set; }=new List<MenuItem>();

        
    }
}
