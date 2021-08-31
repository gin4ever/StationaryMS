using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface ISupplierServices
    {
        List<Supplier> GetSuppliers();
        Supplier AddSupplier(Supplier newSupplier);
        bool UpdateSupplier(Supplier editSupplier);
        Supplier GetSupplier(string code);
        bool DeleteSupplier(string code);
    }
}
