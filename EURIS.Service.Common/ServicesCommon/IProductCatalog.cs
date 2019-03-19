using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Common.ServicesCommon
{
    public interface IProductCatalog
    {
        int ProductCatalogId { get; set; }
        int ProductId { get; set; }
        int CatalogId { get; set; }
    }
}
