using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Extended;
public static partial class Validator
{
    public static bool IsValidCategory(this Category category)
        => !string.IsNullOrWhiteSpace(category.NameUz)
        && !string.IsNullOrWhiteSpace(category.NameRu);

    public static bool IsExist(this Category category, 
                                  IEnumerable<Category> categories)
        => categories.Any(c => c.NameUz == category.NameUz 
                            && c.NameRu == category.NameRu);

    public static bool IsNotUnique(this Category category, 
                                                 IEnumerable<Category> categories)
        => categories.Any(c => c.NameUz == category.NameUz 
                            && c.NameRu == category.NameRu      
                            && c.Id != category.Id);

    public static bool IsValidColor(this Color color)
        => !string.IsNullOrWhiteSpace(color.NameUz)
        && !string.IsNullOrWhiteSpace(color.NameUz);

    public static bool IsExist(this Color color, 
                                  IEnumerable<Color> colors)
        => colors.Any(c => c.NameUz == color.NameUz
                        && c.NameRu == color.NameRu);

    public static bool IsNotUnique(this Color color, 
                                          IEnumerable<Color> colors)
        => colors.Any(c => c.NameUz == color.NameUz
                        && c.NameRu == color.NameRu 
                        && c.Id != color.Id);
}