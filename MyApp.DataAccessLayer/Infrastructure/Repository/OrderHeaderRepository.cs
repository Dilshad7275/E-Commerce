using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyAppWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository


    {

        private ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int Id, string? orderstatus, string? paymentstatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if (order != null)
            {
                order.OrderStatus = orderstatus;
            }
            if (paymentstatus != null)
            {
                order.PaymentStatus = paymentstatus;
            }
        }
    }
}
