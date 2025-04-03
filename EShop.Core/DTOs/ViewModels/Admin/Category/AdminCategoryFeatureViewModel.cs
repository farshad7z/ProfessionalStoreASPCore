using EShop.Core.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminCategoryFeatureViewModel
    {
        public int CategoryId { get; set; } // شناسه دسته‌بندی

        public string CategoryName { get; set; } // نام دسته‌بندی (اختیاری برای نمایش در ویو)

        public IEnumerable<Feature> Features { get; set; } // همه ویژگی‌ها
        public IEnumerable<AdminFeatureViewModel> SelectedFeatures { get; set; } // ویژگی‌های انتخاب‌شده برای دسته
      
        // ویژگی جدید برای افزودن
        public int NewFeatureId { get; set; }
    }
}