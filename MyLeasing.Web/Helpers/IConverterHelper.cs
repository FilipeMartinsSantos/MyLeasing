using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Owner TOOwner(OwnerViewModel model, string path, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);
    }
}
