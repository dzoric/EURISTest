using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Common.ServicesCommon
{
    public interface ICatalog
    {
        int CatalogId { get; set; }
        string Code { get; set; }
        string Description { get; set; }
    }
}
