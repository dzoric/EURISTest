using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;
using EURIS.Service.IRepository;
using EURIS.Service.Common.ServicesCommon;
using System.Data;
using AutoMapper;

namespace EURIS.Service.Repository
{
    public class ProductManager : IProductManager
    {
        private readonly LocalDbEntities _context;

        public ProductManager(LocalDbEntities context)
        {
            _context = context;
        }

        public void CreateProduct(IProduct product)
        {
            _context.Product.Add(Mapper.Map<Product>(product));
        }

        public void DeleteProduct(IProduct product)
        {
            _context.Product.Remove(_context.Product.Find(product.ProductId));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IProduct FindProductById(int id)
        {
            return _context.Product.Find(id);
        }

        public IEnumerable<IProduct> GetProductList()
        {
            return _context.Product.ToList();
        }

        public void UpdateProduct(IProduct product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
