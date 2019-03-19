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

    }
}
