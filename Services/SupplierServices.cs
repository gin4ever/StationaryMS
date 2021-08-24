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

        public Supplier AddSupplier(Supplier newSupplier)
        {
            var model = context.Supplier.SingleOrDefault(m => m.SupplierCode.Equals(newSupplier.SupplierCode));
            if (model == null)
            {
                context.Supplier.Add(newSupplier);
                context.SaveChanges();
                return newSupplier;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteSupplier(string code)
        {
            var model = context.Supplier.SingleOrDefault(m => m.SupplierCode.Equals(code));
            if (model != null)
            {
                context.Supplier.Remove(model);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Supplier GetSupplier(string code)
        {
            var model = context.Supplier.SingleOrDefault(m => m.SupplierCode.Equals(code));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Supplier> GetSuppliers()
        {
            return context.Supplier.ToList();
        }

        public bool UpdateSupplier(Supplier editSupplier)
        {
            var model = context.Supplier.SingleOrDefault(m => m.SupplierCode.Equals(editSupplier.SupplierCode));
            if (model != null)
            {
                model.SupplierName = editSupplier.SupplierName;
                model.ContactName = editSupplier.ContactName;
                model.Phone = editSupplier.Phone;
                model.Status = editSupplier.Status;
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
