using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURIS.Service.UnitOfWork;
using EURISTest.Controllers.IControllerServices;
using EURISTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Controllers.ControllerServices
{
    public class CatalogControllerServices : ICatalogControllerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogControllerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCatalog([Bind(Include = "Code, Description, Products")]CatalogViewModel catalog)
        {
            _unitOfWork.CatalogManager.CreateCatalog(Mapper.Map<Catalog>(catalog));

            foreach (var p in catalog.Products)
            {
                var productCatalog = new ProductCatalog();
                productCatalog.CatalogId = catalog.CatalogId;
                productCatalog.ProductId = p.Id;

                _unitOfWork.ProductCatalogManager.AddProductCatalog(productCatalog);
            }
            _unitOfWork.Complete();
        }

        public void EditCatalog([Bind(Include = "CatalogId, Code, Description, Products")]CatalogViewModel catalog)
        {
            _unitOfWork.CatalogManager.UpdateCatalog(Mapper.Map<Catalog>(catalog));

            foreach (var pc in _unitOfWork.ProductCatalogManager.GetProductCatalogsList())
            {
                if (pc.CatalogId == catalog.CatalogId)
                {
                    _unitOfWork.ProductCatalogManager.DeleteProductCatalog(pc);
                }
            }

            foreach (var p in catalog.Products)
            {
                if (p.Checked)
                {
                    ProductCatalog productCatalog = new ProductCatalog
                    {
                        CatalogId = catalog.CatalogId,
                        ProductId = p.Id
                    };

                    _unitOfWork.ProductCatalogManager.AddProductCatalog(productCatalog);
                }
            }
            _unitOfWork.Complete();
        }

        public CatalogViewModel GetCatalogDetailsIncludingProducts(ICatalog catalog)
        {
            var checkBoxList = _unitOfWork.ProductCatalogManager.GetCheckBoxList(catalog.CatalogId);

            var catalogViewModel = Mapper.Map<CatalogViewModel>(catalog);

            catalogViewModel.Products = checkBoxList;

            return catalogViewModel;
        }

        public CatalogViewModel CreateCatalogEmptyCheckBoxList(Catalog catalog)
        {
            var checkBoxList = _unitOfWork.ProductCatalogManager.GetEmptyCheckBoxList();

            var catalogViewModel = Mapper.Map<CatalogViewModel>(catalog);

            catalogViewModel.Products = checkBoxList;

            return catalogViewModel;
        }
    }
}