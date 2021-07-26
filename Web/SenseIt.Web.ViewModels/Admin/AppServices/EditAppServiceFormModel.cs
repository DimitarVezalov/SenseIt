namespace SenseIt.Web.ViewModels.Admin.AppServices
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class EditAppServiceFormModel : EditAppServiceInputModel, IMapFrom<Service>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Service, EditAppServiceFormModel>()
                .ForMember(x => x.Duration, opt =>
                opt.MapFrom(y => y.Duration.ToString().Substring(0, 5)));
        }
    }
}
