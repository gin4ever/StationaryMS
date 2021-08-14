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
    }
}
