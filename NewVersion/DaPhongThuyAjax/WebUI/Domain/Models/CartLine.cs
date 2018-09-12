using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CartLine
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        public List<CartLine> LineCollection { get; set; }
        public Cart()
        {
            LineCollection = new List<CartLine>();
        }
        public void Add(SanPham product, int quantity)
        {
            var model = LineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (model == null)
            {
                CartLine line = new CartLine() { Product = product, Quantity = quantity };
                LineCollection.Add(line);
            }
            else
            {
                model.Quantity += 1;
            }
        }

        public void Delete(SanPham product)
        {
            var line = LineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line != null)
            {
                LineCollection.Remove(line);
            }

        }
        public void Update(SanPham product, int quantity)
        {
            var line = LineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line != null)
            {
                line.Quantity = quantity;
            }
        }
        public decimal? ComputerTotal()
        {
            decimal? total = 0;
            foreach (var item in LineCollection)
            {
                if (item.Product.Gia > 0)
                {
                    total += item.Quantity * item.Product.Gia;
                }
                else
                {
                    total += item.Quantity * item.Product.Gia;
                }
            }
            return total;
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
        public decimal ComputerTotalQuantity()
        {
            return LineCollection
                .Sum(x => x.Quantity)
                ;
        }
    }
 }
