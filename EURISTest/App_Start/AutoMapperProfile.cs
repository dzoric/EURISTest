using AutoMapper;
using EURIS.Entities;
using EURIS.Service.Common.ServicesCommon;
using EURISTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EURISTest.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IProduct, Product>().ReverseMap();
            CreateMap<ICatalog, Catalog>().ReverseMap();
            CreateMap<IProductCatalog, ProductCatalog>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}