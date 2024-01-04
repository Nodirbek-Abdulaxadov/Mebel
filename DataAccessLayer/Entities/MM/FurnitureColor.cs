namespace DataAccessLayer.Entities.MM;

public class FurnitureColor
{
    public int FurnitureId { get; set; }
    public Furniture? Furniture { get; set; }

    public int ColorId { get; set; }
    public Color? Color { get; set; }
}

