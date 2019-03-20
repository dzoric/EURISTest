using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURISTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EURISTest.Controllers.IControllerServices
{
    public interface ICatalogControllerServices
    {
        void CreateCatalog([Bind(Include = "Code, Description, Products")]CatalogViewModel catalog);
        void EditCatalog([Bind(Include = "CatalogId, Code, Description, Products")]CatalogViewModel catalog);
        CatalogViewModel GetCatalogDetailsIncludingProducts(ICatalog catalog);
        CatalogViewModel CreateCatalogEmptyCheckBoxList(Catalog catalog);
    }
}
