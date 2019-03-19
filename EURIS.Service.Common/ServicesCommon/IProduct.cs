using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service.Common.ServicesCommon
{
    public interface IProduct
    {
        string Code { get; set; }
        string Description { get; set; }
        int ProductId { get; set; }
    }
}
