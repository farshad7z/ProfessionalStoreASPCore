using System;
using System.Collections.Generic;

namespace EShop.Core.ViewModels.Common
{
    /// <summary>
    /// مدل عمومی پاسخ‌دهی برای عملیات‌های مختلف سیستم
    /// </summary>
    /// <typeparam name="T">نوع داده‌ای که قرار است در پاسخ بازگردانده شود</typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// آیا عملیات با موفقیت انجام شده است؟
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// پیام موفقیت یا خطا برای نمایش به کاربر
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// کد وضعیت HTTP یا هر کد سفارشی دیگر
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// داده‌ای که در صورت موفقیت عملیات برگردانده می‌شود
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// لیست خطاهای احتمالی (برای اعتبارسنجی یا دیباگ)
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// سازنده پیش‌فرض
        /// </summary>
        public ResponseModel()
        {
            Errors = new List<string>();
        }

        // ========================
        // متدهای استاتیک کمکی
        // ========================

        /// <summary>
        /// ایجاد پاسخ موفق همراه با داده
        /// </summary>
        public static ResponseModel<T> Success(T data, string message = "عملیات با موفقیت انجام شد", int statusCode = 200)
        {
            return new ResponseModel<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }

        /// <summary>
        /// ایجاد پاسخ موفق بدون داده (مثلاً برای عملیات POST/DELETE)
        /// </summary>
        public static ResponseModel<T> SuccessMessage(string message = "عملیات با موفقیت انجام شد", int statusCode = 200)
        {
            return new ResponseModel<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// ایجاد پاسخ شکست همراه با پیام و لیست خطا
        /// </summary>
        public static ResponseModel<T> Fail(string message, List<string> errors = null, int statusCode = 500)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        /// <summary>
        /// ایجاد پاسخ شکست فقط با پیام
        /// </summary>
        public static ResponseModel<T> Fail(string message, int statusCode)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Errors = new List<string> { message }
            };
        }

        /// <summary>
        /// ایجاد پاسخ شکست از روی لیست خطاها
        /// </summary>
        public static ResponseModel<T> FailFromErrors(List<string> errors, int statusCode = 400)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = "در پردازش درخواست خطایی رخ داد",
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
