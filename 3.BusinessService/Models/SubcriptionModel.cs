using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class SubcriptionModel
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsCustomer { get; set; }
        public string EmailSubcribed { get; set; }
        public string CreatedAt { get; set; }
    }
}
