using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities.Models
{
    public class Slide
    {
        public int SlideId { get; set; }

        [StringLength(100, ErrorMessage = "عنوان اسلاید نباید بیشتر از {1} کاراکتر باشد.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "لینک اسلاید نباید بیشتر از {1} کاراکتر باشد.")]
        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public int DisplayOrder { get; set; } // ترتیب نمایش

        public bool IsActive { get; set; } = true; // آیا اسلاید فعال است؟

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? StartDate { get; set; } // تاریخ شروع نمایش اسلاید
        public DateTime? EndDate { get; set; } // تاریخ پایان نمایش اسلاید
    }
}
