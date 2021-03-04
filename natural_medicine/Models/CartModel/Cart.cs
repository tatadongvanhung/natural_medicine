using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natural_medicine.Models.CartModel
{
    [Serializable]
    public class CartItem
    {
        public product product { get; set; }
        public int quantity { set; get; }
        public int total { set; get; }
    }
    public class Cart
    {
        private List<CartItem> lineCollection = new List<CartItem>();

        public void AddItem(product sp)
        {
            CartItem line = lineCollection.Where(p => p.product.id == sp.id).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartItem
                {
                    product = sp,
                    quantity = 1,
                    total = (int)sp.price
                });
            }
            else
            {
                line.quantity += 1;
                line.total = (int)sp.price * line.quantity;
                if (line.quantity <= 0)
                {
                    lineCollection.RemoveAll(l => l.product.id == sp.id);
                }
            }
        }
        public void UpdateItem(product sp, int quantity)
        {
            CartItem line = lineCollection.Where(p => p.product.id == sp.id).FirstOrDefault();

            if (line != null)
            {
                if (quantity > 0)
                {
                    line.quantity = quantity;
                    line.total = (int)sp.price * quantity;
                }
                else
                {
                    lineCollection.RemoveAll(l => l.product.id == sp.id);
                }
            }
        }
        public void RemoveLine(product sp)
        {
            lineCollection.RemoveAll(l => l.product.id == sp.id);
        }
        public int? ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.product.price * e.quantity);

        }
        public int? ComputeTotalProduct()
        {
            return lineCollection.Sum(e => e.quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartItem> Lines
        {
            get { return lineCollection; }
        }
    }
}