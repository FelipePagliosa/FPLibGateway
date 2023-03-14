using System.Security.Claims;
using System.Text;
using AutoMapper;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.LivroRequests;
using LibraryGateway.Application.Responses;
using LibraryGateway.Application.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

public class LivroConfigService : ILivroConfigService
{
    private const string XApiKey = "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXpsdaAQGSD";
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public LivroConfigService(HttpClient httpClient, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("XApiKey", XApiKey);
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetLivrosResponse> GetAllAsync()
    {
        var livroResponse = new GetLivrosResponse();
        var uri = "https://localhost:7201/api/livro";
        //set api key on request header
        var responseString = await _httpClient.GetStringAsync(uri);
        var livros = JsonConvert.DeserializeObject<List<Livro>>(responseString);

        livroResponse.Livros = livros;
        return livroResponse;
    }

    //get livros by filter 
    public async Task<GetLivrosResponse> GetByFilterAsync(LivroFilter filter)
    {
        var livroResponse = new GetLivrosResponse();

        var todoItemJson = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(filter),
            Encoding.UTF8,
            Application.Json); 

        using var httpResponseMessage = await _httpClient.PostAsync("https://localhost:7201/api/livro/filter", todoItemJson);
        httpResponseMessage.EnsureSuccessStatusCode();
        
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var livros = JsonConvert.DeserializeObject<List<Livro>>(responseString);

        livroResponse.Livros = livros;
        return livroResponse;
    }

    //link a book to a user
    public async Task LinkLivroToUser(int idLivro)
    {
        var idUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var todoItemJson = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(new { idLivro, idUser }),
            Encoding.UTF8,
            Application.Json); 

        using var httpResponseMessage = await _httpClient.PostAsync("https://localhost:7201/api/livro/link", todoItemJson);
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    //get livros by user
    public async Task<GetLivrosResponse> GetByUserAsync()
    {
        var livroResponse = new GetLivrosResponse();
        var idUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var todoItemJson = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(new { idUser }),
            Encoding.UTF8,
            Application.Json); 

        using var httpResponseMessage = await _httpClient.PostAsync("https://localhost:7201/api/livro/user", todoItemJson);
        httpResponseMessage.EnsureSuccessStatusCode();
        
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var livros = JsonConvert.DeserializeObject<List<Livro>>(responseString);

        livroResponse.Livros = livros;
        return livroResponse;
    }


    public async Task Add(LivroInsertRequest request)
    {
        var livro = _mapper.Map<PostLivroResponse>(request);

        var todoItemJson = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(livro),
            Encoding.UTF8,
            Application.Json); 
        string myContent = todoItemJson.ReadAsStringAsync().Result;

        using var httpResponseMessage = await _httpClient.PostAsync("https://localhost:7201/api/livro", todoItemJson);
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task Update(LivroUpdateRequest request)
    {
        var livro = _mapper.Map<PutLivroResponse>(request);

        var todoItemJson = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(livro),
            Encoding.UTF8,
            Application.Json); 
        string myContent = todoItemJson.ReadAsStringAsync().Result;

        using var httpResponseMessage = await _httpClient.PutAsync("https://localhost:7201/api/livro", todoItemJson);
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id)
    {
        using var httpResponseMessage = await _httpClient.DeleteAsync($"https://localhost:7201/api/livro/{id}");
        httpResponseMessage.EnsureSuccessStatusCode();
    }

}