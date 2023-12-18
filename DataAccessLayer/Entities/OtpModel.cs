namespace DataAccessLayer.Entities;
public class OtpModel : BaseEntity
{
    public string PhoneNumber { get; set; } = string.Empty;
    public int Code { get; set; }
    public DateTime ExpirationDate { get; set; } 
        = DateTime.UtcNow.AddMinutes(2);
}