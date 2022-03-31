using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.DTO;

namespace TeaShopBLL.BisnessModels
{
    public class Discount
    {
        private decimal _discountPercent;
        public decimal DiscountPercent => _discountPercent;

        private OrderDTO _order;
        public OrderDTO Order => _order;
        public Discount(decimal discountPercent, OrderDTO order)
        {
            _discountPercent = discountPercent;
            _order = order;
        }

        //public decimal GetDiscount()
        //{
        //    if (_order.OrderNumber == 1)
        //    {
        //        return (_order.TotalProductPrice / 100) * _discountPercent;
        //    }
        //    else return _discountPercent = 0;
        //}
    }
}
