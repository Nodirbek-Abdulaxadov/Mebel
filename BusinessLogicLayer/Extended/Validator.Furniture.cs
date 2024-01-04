using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Extended;
public static partial class Validator
{
    public static bool IsValidFurniture(this Furniture furniture)
        => !string.IsNullOrWhiteSpace(furniture.NameUz)
        && !string.IsNullOrWhiteSpace(furniture.NameRu)
        && !string.IsNullOrWhiteSpace(furniture.DescriptionUz)
        && !string.IsNullOrWhiteSpace(furniture.DescriptionRu)
        && furniture.Price > 0
        && furniture.PreparationDays > 0
        && furniture.CategoryId > 0;

    public static bool IsExist(this Furniture furniture,
                               IEnumerable<Furniture> furnitures)
        => furnitures.Any(f => f.NameUz == furniture.NameUz
                            && f.NameRu == furniture.NameRu
                            && f.CategoryId == furniture.CategoryId);

    public static bool IsNotUnique(this Furniture furniture,
                                   IEnumerable<Furniture> furnitures)
        => furnitures.Any(f => f.NameUz == furniture.NameUz
                            && f.NameRu == furniture.NameRu
                            && f.CategoryId == furniture.CategoryId
                            && f.Id != furniture.Id);
}