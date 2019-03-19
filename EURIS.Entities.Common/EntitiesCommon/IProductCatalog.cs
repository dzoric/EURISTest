using System;
using System.Collections.Generic;
using System.Text;

namespace EURIS.Entities.Common.EntitiesCommon
{
    public interface IProductCatalog
    {
        int ProductCatalogId { get; set; }
        int ProductId { get; set; }
        int CatalogId { get; set; }

    }
}
