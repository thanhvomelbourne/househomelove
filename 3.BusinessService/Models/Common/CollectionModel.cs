using CoreLibrary.Data;
using System.Collections.Generic;

namespace BusinessService.Models
{
    public class CollectionModel<T> where T : class
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize
        {
            get
            {
                return Constant.CommonValue.PageSize20;
            }
        }
        public int CurrenPage { get; set; }
        public bool IsFirstPage
        {
            get
            {
                return CurrenPage == 1;
            }
        }
        public bool IsLastPage
        {
            get
            {
                return CurrenPage == TotalPages;
            }
        }
        public IList<T> Data { get; set; }
    }
}
