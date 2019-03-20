using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Service.IRepository;

namespace EURIS.Service.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductManager ProductManagers { get; set; }

        ICatalogManager CatalogManager { get; set; }

        IProductCatalogManager ProductCatalogManager { get; set; }

        void Complete();
    }
}
