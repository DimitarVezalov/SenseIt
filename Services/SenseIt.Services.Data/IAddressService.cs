using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IAddressService
    {
        Task<int> GetAddressId(string userId ,string town, string street, string number, string zipCode);
    }
}
