using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.IRepository
{
    public interface IProductManager : IDisposable
    {
        void CreateProduct(IProduct product);

        IProduct FindProductById(int id);

        IEnumerable<IProduct> GetProductList();

        void UpdateProduct(IProduct product);

        void DeleteProduct(IProduct product);
    }
}
