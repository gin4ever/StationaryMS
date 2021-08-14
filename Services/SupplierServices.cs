using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class SupplierServices : ISupplierServices
    {
        private StationeryContext context;
        public SupplierServices(StationeryContext context)
        {
            this.context = context;
        }
        public List<Supplier> GetSuppliers()
        {
            return context.Supplier.ToList();
        }
    }
}
