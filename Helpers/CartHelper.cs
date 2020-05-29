using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MusicWebsite.Models;

namespace MusicWebsite.Helpers
{
    public class CartHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        public Cart GetCart()
        {

            //var CartNumber = GetCartNumber();
            //var cart = db.Carts.FirstOrDefault(g => g.CartNumber == CartNumber);
            var cart = new Cart();
            cart.CartNumber= GetCartNumber();
            return cart;
        }
       
        public string GetCartNumber()
        {

            if (HttpContext.Current.Session[Cart.CartSessionKey] == null)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var currentUser = db.Users.Find(userId);

                if (currentUser==null)
                {
                    Guid tempCartNumber = Guid.NewGuid();
                    HttpContext.Current.Session[Cart.CartSessionKey] = tempCartNumber.ToString();
                    //var cart = new Cart();
                    //cart.CartNumber= tempCartNumber.ToString();
                    //db.Carts.Add(cart);
                    //db.SaveChanges();


                }
                else
                {
                    HttpContext.Current.Session[Cart.CartSessionKey] = currentUser.Email;

                }
            }
            return HttpContext.Current.Session[Cart.CartSessionKey].ToString();
        }

    

        //public Cart GetCart(string cartnumber)
        //{
        //    var cart = new Cart();
        //    if (!string.IsNullOrWhiteSpace(cartnumber))
        //    {
        //       cart = db.Carts.FirstOrDefault(c => c.CartNumber == cartnumber);
        //     }
        //    else
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.GetUserId()))
        //        {
        //            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
        //            var User = db.Users.Find(currentUserId);
        //            cart = db.Carts.FirstOrDefault(c => c.CartNumber == User.Email);
        //        }
        //        else
        //        {
        //            Guid tempBasketID = Guid.NewGuid();
        //            cart.CartNumber = tempBasketID.ToString();
                  
        //            db.Carts.Add(cart);
        //            db.SaveChanges();
        //        }
               

        //    }
        //        return cart;
        //}

        public void AddItem(int itemId, int qty, string cartnumber)
        {
            //var cart = db.Carts.FirstOrDefault(g => g.CartNumber == cartnumber);
            var cart = GetCart();
            var item = db.Items.FirstOrDefault(g => g.Id == itemId);
            var cartitem = db.CartItem.FirstOrDefault(b => b.CartId == cartnumber &&
b.itemId == itemId);
            if(cartitem==null)
            {
                var cartitemm = new CartItem();
                cartitemm.itemId = itemId;
                cartitemm.QtyToOrder = qty;
                cartitemm.DateCreated = DateTime.Now;
                cartitemm.CartId = cartnumber;
                cartitemm.CartItemAmount = cartitemm.CartItemAmount + item.Price * qty;
                db.CartItem.Add(cartitemm);
               
                db.SaveChanges();

            }
            else
            {
                
                cartitem.QtyToOrder = cartitem.QtyToOrder + qty;
                cartitem.CartItemAmount= cartitem.CartItemAmount+ item.Price * qty;



            }
           
            db.SaveChanges();

        }

       
        public void EmptyCart()
        {
            var cart = GetCart();

            var cartitems = db.CartItem.Where(b => b.CartId == cart.CartNumber).ToList() ;
            foreach(var cartitem in cartitems)
            {

                db.CartItem.Remove(cartitem);
                db.SaveChanges();
            }
            

        }

    }
}