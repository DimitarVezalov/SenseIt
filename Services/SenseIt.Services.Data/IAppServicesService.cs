using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IAppServicesService
    {
        Task<IEnumerable<T>> GetAllByCategory<T>(int? id);
    }
}
