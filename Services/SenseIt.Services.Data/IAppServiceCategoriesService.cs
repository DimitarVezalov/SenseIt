using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IAppServiceCategoriesService
    {
        Task<IEnumerable<T>> GetAllCategories<T>();
    }
}
