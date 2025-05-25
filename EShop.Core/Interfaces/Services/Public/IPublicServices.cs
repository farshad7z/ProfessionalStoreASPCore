using System.Threading.Tasks;
using EShop.Core.ViewModels.Common;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IPublicServices
    {
        Task<ResponseModel<bool>> IsAccessUserToThisProductAsync(int userId, int productId , int ShopId);
    }
}
