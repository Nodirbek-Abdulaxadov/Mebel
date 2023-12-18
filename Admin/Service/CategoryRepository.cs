using Blazored.LocalStorage;
using Admin.Contracts;
using System.Net.Http;
using DTOs.CategoryDtos;

namespace Admin.Service;

public class CategoryRepository(HttpClient client,
                                ILocalStorageService localStorage) 
    :  BaseRepository<CategoryDto>(client, localStorage), ICategoryRepository
{
    private readonly HttpClient _client = client;
    private readonly ILocalStorageService _localStorage = localStorage;
}
