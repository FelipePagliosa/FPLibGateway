using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGateway.Application.Requests.UserRequests
{
    public class UserChangePasswordRequest
    {
        public int Id { get; set; }
        public string Senha { get; set; }
    }
}