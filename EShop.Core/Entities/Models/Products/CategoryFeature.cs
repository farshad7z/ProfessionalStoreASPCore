using EShop.Core.Entities.Models.Products;
using EShop.Core.Entities.Models;

public class CategoryFeature
{
    public int Id { get; set; }

    public int FeatureId { get; set; } // ارتباط با ویژگی
    public int CategoryId { get; set; } // ارتباط با دسته محصولات
    public bool IsRequired { get; set; } = false; // آیا مقداردهی این ویژگی الزامی‌ست؟
    public bool IsVariant { get; set; } = false; // آیا این ویژگی باعث ایجاد تنوع محصول (Variant) می‌شود؟

    #region Relation
    public ProductCategory Category { get; set; }
    public Feature Feature { get; set; }
    #endregion
}
