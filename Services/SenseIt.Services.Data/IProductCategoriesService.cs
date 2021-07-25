using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IProductCategoriesService
    {
        Task<IEnumerable<string>> GetProductCategories();
    }
}
