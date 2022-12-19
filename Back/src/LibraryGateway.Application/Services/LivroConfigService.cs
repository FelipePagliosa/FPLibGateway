using System.Text;
using AutoMapper;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.LivroRequests;
using LibraryGateway.Application.Responses;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

public class LivroConfigService : ILivroConfigService
{
    private const string XApiKey = "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXpsdaAQGSD";
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;


    public LivroConfigService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("XApiKey", XApiKey);
        _mapper = mapper;
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

}