namespace SenseIt.Web.ViewModels.Admin.Product
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants;

    public class AdminProductDetailsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, AdminProductDetailsViewModel>()

                .ForMember(x => x.CategoryName, opt =>
                opt.MapFrom(y => y.Category.IsDeleted ? MissingCategoryValue : y.Category.Name))

                .ForMember(x => x.Gender, opt =>
                opt.MapFrom(y => y.Gender.ToString()));
        }
    }
}
