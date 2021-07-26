namespace SenseIt.Web.ViewModels.Admin.Product
{
    using System.Collections.Generic;

    public class CreateProductFormModel
    {
        public IEnumerable<string> Genders { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
