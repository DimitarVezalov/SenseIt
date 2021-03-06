namespace SenseIt.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class ProductDetailsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public int InStockQuantity { get; set; }

        public string Description { get; set; }

        public string Gender { get; set; }

        public bool ExistsInCart { get; set; }

        public bool IsAvailable => this.InStockQuantity > 10;

        public string Availability => this.IsAvailable
            ? ProductAvailableString : ProductNotAvailableString;

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, 100, ErrorMessage ="Value should be between 1 and 100")]
        public int CartQuantity { get; set; }

        public IEnumerable<ProductInListViewModel> Products { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(x => x.Gender, opt =>
                opt.MapFrom(y => y.Gender.ToString()));
        }
    }
}
