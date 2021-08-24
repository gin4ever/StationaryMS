using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Repository;
namespace eProject.Services
{
    public class ItemServices : IItemServices
    {
        private StationeryContext context;
        public ItemServices(StationeryContext context)
        {
            this.context = context;
        }

        public Item CreateItem(Item newItem)
        {
            var model = context.Item.SingleOrDefault(i => i.ItemCode.Equals(newItem.ItemCode));
            if (model == null)
            {
                context.Item.Add(newItem);
                context.SaveChanges();
                return newItem;
            }
            else
            {
                return null;
            }
        }

        public Item GetItem(string itemCode)
        {
            var model = context.Item.SingleOrDefault(m => m.ItemCode.Equals(itemCode));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Item> GetItems()
        {
            List<Item> items = context.Item.ToList();
            return items;
        }

        public bool UpdateItem(Item editItem)
        {
            var model = context.Item.SingleOrDefault(i => i.ItemCode.Equals(editItem.ItemCode));
            if (model != null)
            {
                model.Description = editItem.Description;
                model.Category_Id = editItem.Category_Id;
                model.Price = editItem.Price;
                model.SupplierCode = editItem.SupplierCode;
                model.Unit = editItem.Unit;
                model.Stock = editItem.Stock;
                model.Role_Id = editItem.Role_Id;
                model.Images = editItem.Images;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
