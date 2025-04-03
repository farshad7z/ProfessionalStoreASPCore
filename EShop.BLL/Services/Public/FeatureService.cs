using EShop.Core.Entities.Models;
using EShop.Core.Entities.Models.Products;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services.Public
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeatureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddFeatureAsync(Feature model)
        {
            await _unitOfWork.Repository<Feature>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Feature>> GetAllFeaturesAsync()
        {
            return await _unitOfWork.Repository<Feature>().GetAllAsync();
        }

        public async Task<Feature?> GetFeatureByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Feature>().GetByIdAsync(id);
        }

        public async Task UpdateFeatureAsync(Feature model)
        {
            _unitOfWork.Repository<Feature>().Update(model);
            await _unitOfWork.SaveAsync();
        }
    }
}
