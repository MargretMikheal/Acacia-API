using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Data.Entities;

namespace Acacia.Service.Services
{
    /*
    public class ProductSizeService : IProductSizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductSizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _unitOfWork.productSizeRepository.GetAllAsync();
        }

        public async Task<ProductSize?> GetByIdAsync(int id)
        {
            return await _unitOfWork.productSizeRepository.GetByIdAsync(id);
        }

        public async Task<ProductSize> AddAsync(ProductSize productSize)
        {
            await _unitOfWork.productSizeRepository.AddAsync(productSize);
            await _unitOfWork.SaveChangesAsync();
            return productSize;
        }

        public async Task<ProductSize> UpdateAsync(ProductSize productSize)
        {
            _unitOfWork.productSizeRepository.UpdateAsync(productSize);
            await _unitOfWork.SaveChangesAsync();
            return productSize;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.productSizeRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.productSizeRepository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsForProductAsync(int productTypeId, decimal size)
        {
            var allProductSizes = await _unitOfWork.productSizeRepository.GetAllAsync();
            return allProductSizes.Any(ps => ps.ProductTypeId == productTypeId && ps.SizeValue == size);
        }
    }
    */
}
