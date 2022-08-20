﻿using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public  ICartRepository Cart { get; private set; }
        public IApplicationUser ApplicationUser { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product=new ProductRepository(_context);
            Cart = new CartRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            OrderHeader=new OrderHeaderRepository(_context);
            OrderDetail=new OrderDetailRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
