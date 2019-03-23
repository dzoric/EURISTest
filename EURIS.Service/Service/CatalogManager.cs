using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURIS.Service.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Service
{
    public class CatalogManager : ICatalogManager
    {
        private readonly LocalDbEntities _context;

        public CatalogManager(LocalDbEntities context)
        {
            _context = context;
        }

        public void CreateCatalog(ICatalog catalog)
        {
            _context.Catalog.Add(Mapper.Map<Catalog>(catalog));
        }

        public void DeleteCatalog(ICatalog catalog)
        {
            _context.Catalog.Remove(_context.Catalog.Find(catalog.CatalogId));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ICatalog FindCatalogById(int id)
        {
            return _context.Catalog.Find(id);
        }

        public IEnumerable<ICatalog> GetCatalogList()
        {
            return _context.Catalog.ToList();
        }

        public void UpdateCatalog(ICatalog catalog)
        {
            _context.Entry(catalog).State = EntityState.Modified;
        }
    }
}
