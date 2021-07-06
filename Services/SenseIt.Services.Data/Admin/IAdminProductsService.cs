﻿namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models;

    public interface IAdminProductsService
    {
        Task<int> CreateAsync(CreateProductInputModel model);

        Task<IEnumerable<AdminProductsListingViewModel>> GetAllProductsAsync();

        Task<bool> Restock(int? productId);

        Task<bool> Delete(int? productId);

        Task<AdminProductDetailsViewModel> GetDetailsModel(int? id);

        Task<bool> Update(int? id, AdminProductUpdateModel model);
    }
}
