using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.Core.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EShop.BLL.Services.Public
{
    public class PublicServices : IPublicServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublicServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<bool>> IsAccessUserToThisProductAsync(int userId,int productId,int shopId)
        {
            
            bool hasShop= await _unitOfWork.Repository<Employee>().ExistsAsync(e => e.UserId == userId && (e.ShopId != null || e.ShopId != 0));

            if (!hasShop)
            {
                return ResponseModel<bool>.Fail("شما در هیچ فروشگاهی کارمند نیستید", 403);

            }
            bool hasAccess =await _unitOfWork.Repository<Employee>().ExistsAsync(e => e.UserId == userId && e.ShopId == shopId);
            if (!hasAccess)
                return ResponseModel<bool>.Fail("کاربر به این محصول دسترسی ندارد", 403);

            return ResponseModel<bool>.Success(true, "کاربر به محصول دسترسی دارد");
        }
    }
}
