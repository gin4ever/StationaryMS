using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class ItemCategorySupplierServices : IItemCategorySupplierServices
    {
        private StationeryContext context;
        public ItemCategorySupplierServices(StationeryContext context)
        {
            this.context = context;
        }
        public vItemCategorySupplier GetItemCategorySupplier(string itemCode)
        {
            var model = context.vItemCategorySupplier.SingleOrDefault(i => i.ItemCode.Equals(itemCode));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
