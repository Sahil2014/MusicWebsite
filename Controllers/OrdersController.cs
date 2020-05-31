using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicWebsite.Helpers;
using MusicWebsite.Models;

namespace MusicWebsite.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CartHelper carthelper = new CartHelper();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,ShippingAddress,FirstName,LastName,IsShipped,Total,ShippingCharges,GrandTotal")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public ActionResult Confirm(int id)
        {
            var order = db.Orders.Find(id);
            return View(order);
        }
        public ActionResult CheckOut()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order order)
        {
            
            
            var cartnumber=carthelper.GetCartNumber();
           
            order.PlacedOn = DateTime.Now;
            order.IsShipped = false;
            if(order.ShippingAddress.Contains("USA"))
            {
                order.ShippingCharges = 15;
            }
            else
            {
                order.ShippingCharges = 100;
            }
           
            db.Orders.Add(order);
            db.SaveChanges();
            var orderid = order.Id;
            var myCartItems = db.CartItem.Where(u => u.CartId == cartnumber).ToList();
           
            foreach (var cartitem in myCartItems)
            {
                var orderitem = new OrderItem();
                orderitem.itemId = cartitem.itemId;
                orderitem.ItemPrice = cartitem.CartItemAmount;
                orderitem.OrderId = orderid;
                orderitem.QtyToOrder = cartitem.QtyToOrder;
                orderitem.DateCreated = DateTime.Now;
                db.OrderItems.Add(orderitem);
                order.Total = order.Total + cartitem.CartItemAmount;
                
                db.SaveChanges();
            }
            order.GrandTotal = order.Total + order.ShippingCharges;
            db.SaveChanges();




            return RedirectToAction("Confirm","Orders",new { id=order.Id});
            

            
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,ShippingAddress,FirstName,LastName,IsShipped,Total,ShippingCharges,GrandTotal")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
