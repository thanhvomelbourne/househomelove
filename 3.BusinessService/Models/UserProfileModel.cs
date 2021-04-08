using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string GenderDisplay { get; set; }
        public string CompanyName { get; set; }
        public bool IsAdmin { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsLive { get; set; }
        public string Avatar { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Role { get; set; }
    }
}
