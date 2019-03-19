using System;
using System.Collections.Generic;
using System.Text;

namespace EURIS.Entities.Common.EntitiesCommon
{
    public interface ICatalog
    {
        int CatalogId { get; set; }
        string Code { get; set; }
        string Description { get; set; }
    }
}
