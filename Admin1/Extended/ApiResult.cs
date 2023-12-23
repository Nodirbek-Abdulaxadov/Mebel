namespace Admin1.Extended;

public class ApiResult<T>(bool IsSuccess, 
                          string Message, 
                          T model,
                          List<T> Data = null)
{
    public bool IsSuccess = IsSuccess;
    public string Message = Message;
    public List<T> Data = Data;
    public T Model = model;
}