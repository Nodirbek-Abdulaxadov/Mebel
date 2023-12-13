using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Extended;
public static class Validator
{
    public static bool IsValidCategory(this Category category)
        => !string.IsNullOrWhiteSpace(category.Name);

    public static bool IsExist(this Category category, 
                                  IEnumerable<Category> categories)
        => !categories.Any(c => c.Name == category.Name);

    public static bool IsNotUnique(this Category category, 
                                                 IEnumerable<Category> categories)
        => categories.Any(c => c.Name == category.Name && c.Id != category.Id);

    public static bool IsValidColor(this Color color)
        => !string.IsNullOrWhiteSpace(color.Name);

    public static bool IsExist(this Color color, 
                                  IEnumerable<Color> colors)
        => !colors.Any(c => c.Name == color.Name);

    public static bool IsNotUnique(this Color color, 
                                          IEnumerable<Color> colors)
        => colors.Any(c => c.Name == color.Name && c.Id != color.Id);
}