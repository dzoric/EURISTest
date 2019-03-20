using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURIS.Service.UnitOfWork;
using EURISTest.Controllers.ControllerServices;
using EURISTest.Controllers.IControllerServices;
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
        private readonly ICatalogControllerServices _catalogControllerServices;
        public CatalogController(IUnitOfWork unitOfWork, ICatalogControllerServices catalogControllerServices)
        {
            _unitOfWork = unitOfWork;
            _catalogControllerServices = catalogControllerServices;
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.CatalogManager.GetCatalogList());
        }

        public ActionResult Create(Catalog catalog)
        {
            return View(_catalogControllerServices.CreateCatalogEmptyCheckBoxList(catalog));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code, Description, Products")]CatalogViewModel catalog)
        {
            if (ModelState.IsValid)
            {
                _catalogControllerServices.CreateCatalog(catalog);
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

                return View(_catalogControllerServices.GetCatalogDetailsIncludingProducts(catalog));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatalogId, Code, Description, Products")] CatalogViewModel catalog)
        {
            if (ModelState.IsValid)
            {
                _catalogControllerServices.EditCatalog(catalog);
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
            return View(_catalogControllerServices.GetCatalogDetailsIncludingProducts(catalog));
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

            return View(_catalogControllerServices.GetCatalogDetailsIncludingProducts(catalog));
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
