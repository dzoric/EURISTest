using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using EURIS.Service.Service;
using EURIS.Service.IService;

namespace EURIS.Service.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LocalDbEntities _context;

        public IProductManager ProductManagers { get; set; }

        public ICatalogManager CatalogManager { get; set; }

        public IProductCatalogManager ProductCatalogManager { get; set; }

        public UnitOfWork(LocalDbEntities context)
        {
            _context = context;
            ProductManagers = new ProductManager(context);
            CatalogManager = new CatalogManager(context);
            ProductCatalogManager = new ProductCatalogManager(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
