using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationCore.Models
{
    public class PagedResultSet<T> where T : class
    {
        public int PageIndex { get; set; }// Current page number
        public int PageSize { get; set; }// Number of items per page
        public int TotalPages { get; set; }          // Total number of pages
        public long Count { get; set; } // Total number of items
        public bool HasPreviousPage => PageIndex > 1; // Corrected comparison
        public bool HasNextPage => PageIndex < TotalPages; // Corrected comparison
        public IEnumerable<T> Data { get; set; } // The data for the current page
        public PagedResultSet(IEnumerable<T> data, int pageIndex, int pageSize, long count)
        { // Constructor to initialize the paged result set
            PageIndex = pageIndex; // Current page number
            PageSize = pageSize; // Number of items per page
            Count = count; // Total items
            Data = data; // Data for the current page
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Calculate total pages

        }
    }

    
    
}
