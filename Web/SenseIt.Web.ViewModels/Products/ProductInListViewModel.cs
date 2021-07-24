namespace SenseIt.Web.ViewModels.Products
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class ProductInListViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductInListViewModel>()
                .ForMember(x => x.Gender, opt =>
                opt.MapFrom(y => y.Gender.ToString()));
        }
    }
}
