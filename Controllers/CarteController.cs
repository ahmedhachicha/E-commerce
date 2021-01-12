using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    public class CarteController : Controller
    {
        private readonly DBaseTechProducts _dBaseTechProduct;
        Carte carte;
        string[] Cities = { "Ariana", "Béja", "Ben Arous", "Bizerte", "Gabès", "Gafsa", "Jendouba", "Kairouan", "Kasserine", "Kebili", "Kef", "Mahdia", "Manouba", "Medenine", "Monastir", "Nabeul", "Sfax", "Sidi Bouzid", "Siliana", "Sousse", "Tataouine", "Tozeur", "Tunis", "Zaghouan" };
        
        public CarteController(DBaseTechProducts dBaseTechProduct)
        {
            _dBaseTechProduct = dBaseTechProduct;
            carte = new Carte(_dBaseTechProduct);
        }
      
        public IActionResult AddToCarte(int productId,string returnUrl)
        {
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productId);

            
            if (product != null)
            {
                carte.AddItem(product, 1);
                
                Carte.returnUrl = returnUrl;
            }
            
            //return View();
            return RedirectToAction(nameof(Index)) ;
        }
        public IActionResult RemoveFromCarte(int productId,string returnUrl)
        {
            
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                carte.RemoveLine(product);
            }
            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Index()
        {
           
            
            if (User.Identity != null && User.IsInRole("USER"))
            {

                ViewBag.returnUrl = Carte.returnUrl;
                ViewData.Add("cartItems", Carte.Lines2);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public PartialViewResult Summary(Carte carte)
        {
            
            return PartialView(carte);
        }
        public IActionResult Order()
        {
            
            new SelectListItem { Text = "Football", Value = "Football" };
            ViewData.Add("CitiesList", getCitiesList());
            return View();
        }
        [HttpPost]
        public IActionResult Order(ShippingDetail shippingDetail)
        {
            
            if (Carte.Lines.Count() == 0)
                ModelState.AddModelError("Empty", "Sorry, your carte is empty!");
            if (ModelState.IsValid)
            {
                shippingDetail.FullName = _dBaseTechProduct.dbUsers.FirstOrDefault(user => user.FullName == User.FindFirst("FullName").Value).FullName;
                shippingDetail.orderDate = System.DateTime.Now;
                shippingDetail.buyer = _dBaseTechProduct.dbUsers.FirstOrDefault(user => user.FullName == User.FindFirst("FullName").Value);
                shippingDetail.state = false;
                shippingDetail.orders = (List<CarteLine>)Carte.Lines;
                _dBaseTechProduct.dbOrders.Add(shippingDetail);
                _dBaseTechProduct.SaveChanges();
                Carte.Clear();
                ViewData.Add("CitiesList", getCitiesList());
                return RedirectToAction(nameof(Complited));

            }
            else
                ModelState.AddModelError("Empty", "Gov ="+shippingDetail.Gouvernerat);
                ViewData.Add("CitiesList", getCitiesList());
                return View(shippingDetail);
        }
        public IActionResult Complited()
        {

            ViewData.Add("CitiesList", getCitiesList());
            return View();
        }
        public IEnumerable<SelectListItem> getCitiesList()
        {
            List<SelectListItem> CitiesList = new List<SelectListItem>();
            foreach(string s in Cities)
            {
                CitiesList.Add(new SelectListItem { Text = s, Value = s });
            }   
            return CitiesList;

        }
    }
}
