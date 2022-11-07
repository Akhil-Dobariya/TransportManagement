using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models
{
    public class UserModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Permissions { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }

}
