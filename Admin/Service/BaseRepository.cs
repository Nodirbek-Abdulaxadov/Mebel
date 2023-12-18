using Blazored.LocalStorage;
using Admin.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Admin.Service;

public class BaseRepository<T> (HttpClient client,
                                ILocalStorageService localStorage)
    : IBaseRepository<T> where T : class
{
    private readonly HttpClient _client = client;
    private readonly ILocalStorageService _localStorage = localStorage;
    private string url = "https://localhost:44305/api/";
    
    public async Task<bool> Create(T obj)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetBearerToken());
        HttpResponseMessage response = await _client.PostAsJsonAsync<T>(url, obj);
        if (response.StatusCode == System.Net.HttpStatusCode.Created)
            return true;

        return false;
    }

    public async Task<bool> Delete(int id)
    {
        if (id < 1)
            return false;

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetBearerToken());
        HttpResponseMessage response = await _client.DeleteAsync(url + id);
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return true;

        return false;
    }

    public async Task<T> Get(int id)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetBearerToken());
        var reponse = await _client.GetFromJsonAsync<T>(url + id);

        return reponse;
    }

    public async Task<IList<T>> Get()
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var reponse = await _client.GetFromJsonAsync<IList<T>>(url);

            return reponse;

        }
        catch (Exception)
        {
            return null;
           
        }
        


        
    }

    public async Task<bool> Update(T obj, int id)
    {
        if (obj == null)
            return false;

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetBearerToken());
        var response = await _client.PutAsJsonAsync<T>(url + id, obj);
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return true;

        return false;
    }

    private async Task<string> GetBearerToken()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }
}
