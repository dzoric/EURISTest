﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using EURIS.Service.Repository;
using EURIS.Service.IRepository;

namespace EURIS.Service.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LocalDbEntities _context;

        public IProductManager ProductManagers { get; set; }

        public UnitOfWork(LocalDbEntities context)
        {
            _context = context;
            ProductManagers = new ProductManager(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}