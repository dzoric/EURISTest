using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURIS.Service.UnitOfWork;
using EURISTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CatalogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var catalogsList = _unitOfWork.CatalogManager.GetCatalogList();

            ViewBag.Catalogs = Mapper.Map<IEnumerable<CatalogViewModel>>(catalogsList);

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code, Description")]CatalogViewModel catalog)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CatalogManager.CreateCatalog(Mapper.Map<Catalog>(catalog));
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Catalog");
            }

            return View(catalog);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var catalog = _unitOfWork.CatalogManager.FindCatalogById((int)id);
                if (catalog == null)
                {
                    return HttpNotFound();
                }

                var checkBoxList = _unitOfWork.ProductCatalogManager.GetCheckBoxList((int)id);

                var catalogViewModel = Mapper.Map<CatalogViewModel>(catalog);

                catalogViewModel.Products = checkBoxList;

                return View(catalogViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatalogId, Code, Description, Products")] CatalogViewModel catalog)
        {
            if (ModelState.IsValid)
            {
                var catalogEntity = Mapper.Map<Catalog>(catalog);

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
                        var productCatalog = new ProductCatalog();
                        productCatalog.CatalogId = catalog.CatalogId;
                        productCatalog.ProductId = p.Id;

                        _unitOfWork.ProductCatalogManager.AddProductCatalog(productCatalog);
                    }
                }

                _unitOfWork.Complete();
                return RedirectToAction("Index", "Catalog");
            }

            return View(catalog);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var catalog = _unitOfWork.CatalogManager.FindCatalogById((int)id);
                if (catalog == null)
                {
                    return HttpNotFound();
                }
                return View(Mapper.Map<CatalogViewModel>(catalog));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var catalog = _unitOfWork.CatalogManager.FindCatalogById(id);
            _unitOfWork.CatalogManager.DeleteCatalog(Mapper.Map<Catalog>(catalog));
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Catalog");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalog = _unitOfWork.CatalogManager.FindCatalogById((int)id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            var checkBoxList = _unitOfWork.ProductCatalogManager.GetCheckBoxList((int)id);

            var catalogViewModel = Mapper.Map<CatalogViewModel>(catalog);

            catalogViewModel.Products = checkBoxList;

            return View(catalogViewModel);
        }

        public ActionResult ViewProducts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalog = _unitOfWork.CatalogManager.FindCatalogById((int)id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            var checkBoxList = _unitOfWork.ProductCatalogManager.GetCheckBoxList((int)id);

            var catalogViewModel = Mapper.Map<CatalogViewModel>(catalog);

            catalogViewModel.Products = checkBoxList;

            return View(catalogViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.CatalogManager.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
