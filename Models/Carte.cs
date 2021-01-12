using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class Carte
    {
        private static List<CarteLine> lineCollection = new List<CarteLine>();
        private static List<CarteLine2> lineCollection2 = new List<CarteLine2>();
        private static IEnumerable<Product> prodList;
        public static string returnUrl;
        public Carte(DBaseTechProducts dBaseTechProduct)
        {
            prodList = dBaseTechProduct.dbProducts.ToList();
        }

        public void AddItem(Product product,int quantity)
        {
            Console.WriteLine("Add Item ="+product.ProductID +" q="+quantity );
            CarteLine line = lineCollection.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            CarteLine2 line2 = lineCollection2.FirstOrDefault(pp2 => pp2.product.ProductID == product.ProductID);
            if (line == null)
            {
                lineCollection.Add(new CarteLine
                {
                    ProductID = product.ProductID,
                    Quantity = quantity
                });

            }
            else
            {
                line.Quantity += quantity;
                
            }
            if (line2 == null)
            {
                Console.WriteLine("Add Item =" + product.ProductID + " q=" + quantity);

                lineCollection2.Add(new CarteLine2
                {
                    product = prodList.First(ps => ps.ProductID == product.ProductID),
                    Quantity = quantity
                });
            }
            else
            {
                
                line2.Quantity += quantity;
            }

        }
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(p => p.ProductID == product.ProductID);
            lineCollection2.RemoveAll(p => p.product.ProductID == product.ProductID);
        }
        public static decimal CompTotalValue()
        {
            return lineCollection.Sum(p => prodList.First(ps => ps.ProductID == p.ProductID).Price * p.Quantity);
        }
        public static void Clear()
        {
            lineCollection.Clear();
            lineCollection2.Clear();
        }
        public static IEnumerable<CarteLine> Lines
        {
            get { return lineCollection; }
        }
        public static IEnumerable<CarteLine2> Lines2
        {
            get { return lineCollection2; }
        }

        

    }
}
