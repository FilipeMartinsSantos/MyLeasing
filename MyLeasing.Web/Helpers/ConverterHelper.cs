using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System.IO;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner TOOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id, 
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                ImageUrl = path,
                User = model.User,
                Document = model.Document,
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                ImageUrl = owner.ImageUrl,
                User = owner.User,
                Document = owner.Document,
            };
        }
    }
}
