using EShop.Core.DTOs.ViewModels.Public;
using EShop.Core.DTOs.ViewModels.Public.Account;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services.Public
{
   public class AccountServices : IAccountServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetUserByMobileNumberAsync(string mobileNumber)
        {
            return await _unitOfWork.Repository<User>().FindSingleOrDefaultAsync(u => u.PhoneNumber == mobileNumber);
     
        }

        public Task<bool> IsExistMobileNumberAsync(string mobileNumber)
        {
            return _unitOfWork.Repository<User>().ExistsAsync(p => p.PhoneNumber == mobileNumber);
        }

        public async Task<int>? RegisterUserAsync(User model)
        {
            await _unitOfWork.Repository<User>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.UserId;


        }


       public async Task UpdateUserAsync(User model)
        {
             _unitOfWork.Repository<User>().Update(model);
              await _unitOfWork.SaveAsync();
        }
    }
}
