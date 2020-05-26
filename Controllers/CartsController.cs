using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicWebsite.Helpers;
using MusicWebsite.Models;

namespace MusicWebsite.Controllers
{[Authorize]
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CartHelper carthelper = new CartHelper();
        public ActionResult Buy(int ItemId)
        {
            var item = db.Items.FirstOrDefault(u => u.Id == ItemId);
            return View(item);

        }
        // GET: Carts
        public ActionResult AddItemToCart(int ItemId, int qty)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);
            var cartId = currentUser.Email;
        
                var item = db.Items.FirstOrDefault(u => u.Id == ItemId);
            if (item.Qty >= qty)
            {
                carthelper.AddItem(ItemId, qty, cartId);


                return RedirectToAction("MyCart", "CartItems", new { cartId = cartId });
            }
            else
            {
                return  RedirectToAction("OutofStock", "Items");
            }

            
        }
        




    }
}