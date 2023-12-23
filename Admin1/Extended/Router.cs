namespace Admin1.Extended;

public class Router<T> where T : class
{
    public string BASE = $"{Constants.BASE_URL}/{typeof(T).Name}";
    public string GetAll() =>$"{BASE}/all";
    public string GetAll(int pageSize, int pageNumber) => $"{BASE}/paged?pageSize={pageSize}&pageNumber={pageNumber}";
    public string GetById(int id) => $"{Constants.BASE_URL}/{typeof(T).Name}/{id}";
    public string Delete(int id) => $"{Constants.BASE_URL}/{typeof(T).Name}/{id}";
}