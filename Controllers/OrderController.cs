using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    public class OrderController : Controller
    {
        private readonly DBaseTechProducts _dBaseTechProduct;
        Carte carte;
        
        public OrderController(DBaseTechProducts dBaseTechProduct)
        {
            _dBaseTechProduct = dBaseTechProduct;
            carte = new Carte(_dBaseTechProduct);
        }
      
        public IActionResult Index()
        {
            
            if (User.Identity != null && User.IsInRole("Admin"))
            {

                ViewBag.returnUrl = Carte.returnUrl;
                List<ShippingDetail> ordersList = new List<ShippingDetail>();
                ordersList = _dBaseTechProduct.dbOrders.ToList();
                List<CarteLine2> ordredProductsList  = new List<CarteLine2>();
                var ordredProductsIDList = _dBaseTechProduct.CarteLine.ToList();
                foreach (CarteLine iis in ordredProductsIDList)
                {
                    ordredProductsList.Add(new CarteLine2
                    {
                     product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == iis.ProductID),
                     Quantity = iis.Quantity
                    });
                }
                ViewData.Add("ordredProductsList", ordredProductsList);
                return View(ordersList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
       
        [HttpPost]
        public IActionResult Confirm(int oid)
        {
            Debug.WriteLine("id=" + oid);
            //CODE
            var order = _dBaseTechProduct.dbOrders.FirstOrDefault(o => o.id == oid);
            if (order != null)
            {

                order.state = true;
                _dBaseTechProduct.SaveChanges();
            }
            return RedirectToAction("Index", "Order");
        }
        public IActionResult Delete(ShippingDetail shippingDetail)
        { 
            _dBaseTechProduct.CarteLine.RemoveRange(_dBaseTechProduct.CarteLine.Where(e => e.ShippingDetailid == shippingDetail.id));
            _dBaseTechProduct.dbOrders.Remove(shippingDetail);
            _dBaseTechProduct.SaveChanges();

            return RedirectToAction("Index", "Order");
        }
    }
}
