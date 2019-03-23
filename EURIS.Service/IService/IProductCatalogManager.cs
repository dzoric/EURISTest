using EURIS.Service.Common.ManagerCommon;
using EURIS.Service.Common.ServicesCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.IService
{
    public interface IProductCatalogManager
    {
        IList<CheckBoxViewModel> GetCheckBoxList(int id);

        IEnumerable<IProductCatalog> GetProductCatalogsList();

        void DeleteProductCatalog(IProductCatalog productCatalog);

        void AddProductCatalog(IProductCatalog productCatalog);

        IList<CheckBoxViewModel> GetEmptyCheckBoxList();
    }
}
