using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGateway.Application.Requests.LivroRequests;

namespace LibraryGateway.Application.Interfaces;

public interface ILivroConfigService
{
public Task<GetLivrosResponse> GetAllAsync();
public Task Add(LivroInsertRequest request);
}
