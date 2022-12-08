using System;

namespace LibraryGateway.Application.Requests.UserRequests;

public class UserLoginRequest
{
    public string UserName { get; set; }
    public string Senha { get; set; }
}

