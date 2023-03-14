using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGateway.Application.Requests.LivroRequests;

namespace LibraryGateway.Application.Interfaces;

public interface ILivroConfigService
{
    Task<GetLivrosResponse> GetAllAsync();
    Task Add(LivroInsertRequest request);
    Task Update(LivroUpdateRequest request);
    Task Delete(int id);
    Task<GetLivrosResponse> GetByFilterAsync(LivroFilter filter);
    Task LinkLivroToUser(int livroId);
    Task<GetLivrosResponse> GetByUserAsync();
}
