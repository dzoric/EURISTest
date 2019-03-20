using EURIS.Service.Common.ManagerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EURISTest.ViewModels
{
    public class CatalogViewModel
    {
        public int CatalogId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public IList<CheckBoxViewModel> Products { get; set; }
    }
}