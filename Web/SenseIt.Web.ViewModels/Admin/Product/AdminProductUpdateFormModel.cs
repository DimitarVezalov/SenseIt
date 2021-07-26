namespace SenseIt.Web.ViewModels.Admin.Product
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminProductUpdateFormModel : AdminProductUpdateModel, IMapFrom<Product>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, AdminProductUpdateFormModel>()
                .ForMember(x => x.Gender, opt =>
                opt.MapFrom(y => y.Gender.ToString()));
        }
    }
}
