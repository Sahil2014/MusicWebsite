using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicWebsite.Helpers;
using MusicWebsite.Models;
using MusicWebsite.ViewModels;

namespace MusicWebsite.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CartHelper carthelper = new CartHelper();
        public ActionResult Buy(int ItemId)
        {
            var item = db.Items.FirstOrDefault(u => u.Id == ItemId);
                       
            return View(item);
        }

        public ActionResult AddQty(int ItemId)
        {
            var item = db.Items.FirstOrDefault(u => u.Id == ItemId);
            var buyitem = new BuyItem();
            buyitem.Id = ItemId;
            buyitem.ItemPrice = item.Price;
            return View(buyitem);
        }
        [HttpPost]
        public ActionResult AddQty(BuyItem buyitem)
        {
           
            return RedirectToAction("AddItemToCart",new { ItemId=buyitem.Id,qty=buyitem.QtyToOrder});
        }
        // GET: Carts
        public ActionResult AddItemToCart(int ItemId, int qty)
        {
            
            var cart = carthelper.GetCart();
            var cartId = cart.CartNumber;
        
                var item = db.Items.FirstOrDefault(u => u.Id == ItemId);
            if (item.Qty >= qty)
            {
                carthelper.AddItem(ItemId, qty, cartId);


                return RedirectToAction("MyCart", "CartItems");
            }
            else
            {
                return  RedirectToAction("OutofStock", "Items");
            }

            
        }

        public ActionResult RemoveItemFromCart(int ItemId)
        {

            var cart = carthelper.GetCart();

            var cartitem = db.CartItem.FirstOrDefault(b => b.CartId == cart.CartNumber && b.itemId == ItemId);
            
            if (cartitem != null)
            {
                
                db.CartItem.Remove(cartitem);
                db.SaveChanges();
               
            
            
            //if (cart.ItemsInCart <= 0)
            //    {db.Carts.Remove(cart);
            //    db.SaveChanges(); }
                
            }
            else
            {
                return RedirectToAction("Oops", "Carts");
            }
            return RedirectToAction("MyCart", "CartItems");
           

        }

        public ActionResult EmptyCart()
        {

            carthelper.EmptyCart();

            
            return RedirectToAction("Index", "Home");


        }

        public ActionResult Oops()
        {

            return View();


        }





    }
}