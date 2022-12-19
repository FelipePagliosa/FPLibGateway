using System;
using AutoMapper;
using LibraryGateway.Application.Requests.LivroRequests;
using LibraryGateway.Application.Responses;
using LibraryGateway.Domain.Models;

namespace LibraryGateway.Application.Helpers;
public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<PostLivroResponse, LivroInsertRequest>().ReverseMap();
    }
}

