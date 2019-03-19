using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Repository;
using EURIS.Service.UnitOfWork;
using AutoMapper;
using EURISTest.ViewModels;
using System.Net;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var productsList = _unitOfWork.ProductManagers.GetProductList();

            ViewBag.Products = Mapper.Map<IEnumerable<ProductViewModel>>(productsList);

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code, Description")]ProductViewModel product)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.ProductManagers.CreateProduct(Mapper.Map<Product>(product));
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Product");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var product = _unitOfWork.ProductManagers.FindProductById((int)id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(Mapper.Map<ProductViewModel>(product));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId, Code, Description")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductManagers.UpdateProduct(Mapper.Map<Product>(product));
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Product");
            }

            return View(product);
        }
    }
}
