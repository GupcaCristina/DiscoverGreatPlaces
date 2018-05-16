using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DomainUtil
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get;  set; }
        public int TotalPages { get; set; }
        public PaginatedList()
        {

        }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static List<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
          
            var items =  source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return items;
        }
    }
}