using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ManagerCommon;
using EURIS.Service.Common.ServicesCommon;
using EURIS.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Repository
{
    public class ProductCatalogManager : IProductCatalogManager
    {
        private readonly LocalDbEntities _context;

        public ProductCatalogManager(LocalDbEntities context)
        {
            _context = context;
        }

        public IList<CheckBoxViewModel> GetCheckBoxList(int id)
        {
            var results = from p in _context.Product
            select new
            {
                p.ProductId,
                p.Code,
                p.Description,
                Checked = ((from pc in _context.ProductCatalog
                            where (pc.CatalogId == id) & (pc.ProductId == p.ProductId)
                            select pc).Count() > 0)
            };

            var checkedBoxList = new List<CheckBoxViewModel>();

            foreach (var r in results)
            {
                checkedBoxList.Add(new CheckBoxViewModel { Id = r.ProductId, Code = r.Code, Checked = r.Checked });
            }

            return checkedBoxList;
        }

        public IList<CheckBoxViewModel> GetEmptyCheckBoxList()
        {
            var results = from p in _context.Product
                          select new
                          {
                              p.ProductId,
                              p.Code,
                              p.Description,
                              Checked = ((from pc in _context.ProductCatalog
                                          select pc).Count() > 0)
                          };

            var checkedBoxList = new List<CheckBoxViewModel>();

            foreach (var r in results)
            {
                checkedBoxList.Add(new CheckBoxViewModel { Id = r.ProductId, Code = r.Code });
            }

            return checkedBoxList;
        }

        public IEnumerable<IProductCatalog> GetProductCatalogsList()
        {
            return _context.ProductCatalog.ToList();
        }

        public void DeleteProductCatalog(IProductCatalog productCatalog)
        {
            _context.Entry(productCatalog).State = EntityState.Deleted;
        }

        public void AddProductCatalog(IProductCatalog productCatalog)
        {
            _context.ProductCatalog.Add(Mapper.Map<ProductCatalog>(productCatalog));
        }
    }
}
