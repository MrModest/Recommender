using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class PagingInfo
    {
        public PagingInfo(int totalItems, int itemPerPage, int currentPage)
        {
            this.TotalItems = totalItems;
            this.ItemsPerPage = itemPerPage;
            this.CurrentPage = currentPage;
        }

        public int TotalItems   { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int CurrentPage  { get; private set; }

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        public bool HasPreviousPage
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }
    }
}
