using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
	public class PageViewModel
	{
		//public int TotalProducts { get; set; }
		//public int ProductsPerPage { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
        //{
        //	get { return (int)Math.Ceiling((decimal)TotalProducts / ProductsPerPage); }
        //}

        public PageViewModel(int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
    }
}
