using EURIS.Service.Common.ServicesCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.IService
{
    public interface ICatalogManager : IDisposable
    {
        void CreateCatalog(ICatalog catalog);

        ICatalog FindCatalogById(int id);

        IEnumerable<ICatalog> GetCatalogList();

        void UpdateCatalog(ICatalog catalog);

        void DeleteCatalog(ICatalog catalog);
    }
}
