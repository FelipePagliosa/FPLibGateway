using System;
using AutoMapper;
using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Models;

namespace LibraryGateway.Application.Helpers;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserUpdateRequest>().ReverseMap();
        CreateMap<User, UserInsertRequest>().ReverseMap();
    }
}

