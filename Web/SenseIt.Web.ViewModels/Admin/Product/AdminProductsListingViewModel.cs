namespace SenseIt.Web.ViewModels.Admin.Product
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants;

    public class AdminProductsListingViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public bool IsDeleted { get; set; }

        public char Gender { get; set; }

        public decimal Price { get; set; }

        public int InStockQuantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, AdminProductsListingViewModel>()

                .ForMember(x => x.Gender, opt =>
                opt.MapFrom(y => ((int)y.Gender) == 1 ? 'M' : ((int)y.Gender) == 2 ? 'F' : 'U'))

                .ForMember(x => x.CategoryName, opt =>
                opt.MapFrom(y => y.Category.IsDeleted ? MissingCategoryValue : y.Category.Name));
        }
    }
}
