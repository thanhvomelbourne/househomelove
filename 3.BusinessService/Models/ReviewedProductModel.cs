using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ReviewedProductModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string UserAvatar { get; set; }
        public string UserFullname { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string CreatedAt { get; set; }
    }
}
