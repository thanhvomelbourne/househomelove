using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class ProductImageFiles
    {
        public HttpPostedFileBase PrimaryImage { get; set; }
        public HttpPostedFileBase SubImage1 { get; set; }
        public HttpPostedFileBase SubImage2 { get; set; }
        public HttpPostedFileBase SubImage3 { get; set; }
        public HttpPostedFileBase SubImage4 { get; set; }
        public HttpPostedFileBase SubImage5 { get; set; }
    }
}