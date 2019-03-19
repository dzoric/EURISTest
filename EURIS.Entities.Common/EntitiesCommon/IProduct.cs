using System;
using System.Collections.Generic;
using System.Text;

namespace EURIS.Entities.Common.EntitiesCommon
{
    public interface IProduct
    {
        string Code { get; set; }
        string Description { get; set; }
        int ProductId { get; set; }
    }
}
