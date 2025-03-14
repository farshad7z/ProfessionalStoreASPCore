using EShop.Core.DTOs.ViewModels.Public;
using EShop.Core.DTOs.ViewModels.Public.Account;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services
{
    public interface IAccountServices
    {
        /// <summary>
        /// بررسی می‌کند که آیا شماره موبایل در سیستم وجود دارد یا خیر.
        /// </summary>
        Task<bool> IsExistMobileNumberAsync(string MobileNumber);

          /// <summary>
        /// دریافت اطلاعات حساب کاربری بر اساس شماره موبایل.
        /// </summary>
        Task<User?> GetUserByMobileNumberAsync(string MobileNumber);
        /// <summary>
        /// دریافت اطلاعات حساب کاربری بر اساس Id.
        /// </summary>
        Task<User?> GetUserByIdAsync(int userId);

        /// <summary>
        ///ثبت نام کاربر جدید.
        /// </summary>
        Task<int>? RegisterUserAsync(User model);

        /// <summary>
        ///اپدیت اطلاعات کاربر .
        /// </summary>
        Task UpdateUserAsync(User model);

        /// <summary>
        ///'دریاقت اطلاعات حساب کارمند'.
        /// </summary>
        Task<Employee>? GetDetailsEmployeeByUserIdAsync(int UserId);

        /// <summary>
        ///'بررسی وجود و صحت اطلاعات حساب کارمند'.
        /// </summary>
        Task<bool> IsExistsEmployeeByUserIdAsync(int UserId);
        /// <summary>
        ///اپدیت اطلاعات کارمند.
        /// </summary>
        Task UpdateEmployeeAsync(Employee model);
    }
}
