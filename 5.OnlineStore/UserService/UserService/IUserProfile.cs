using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.UserService
{
    public interface IUserProfile<TUserId>
        where TUserId : struct
    {
        TUserId UserId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string CompanyName { get; set; }
        string Address { get; set; }
        string Suburb { get; set; }
        string State { get; set; }
        string Postcode { get; set; }
        string Country { get; set; }
        bool IsLive { get; set; }
        DateTime? CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}
