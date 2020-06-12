using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MusicWebsite.Helpers;
using MusicWebsite.Models;


namespace MusicWebsite.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AudioHelper audioHelper = new AudioHelper();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.category);
            var categories = db.Categories;
            ViewBag.categories = new MultiSelectList(categories, "Id", "Genre");
           
            return View(items.ToList());
        }

       
        public ActionResult FilteredIndex(List<int> categories)
        {
            var items = new List <Item>();
            
            if (categories!=null)
            {
                foreach(var category in categories)

                {
                    var catitem = db.Items.Where(p => p.CategoryId == category).ToList();
                    items.AddRange(catitem);
                }
                return View(items.ToList());
            }


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult SortedIndex(int sortby)
        {
            var items = db.Items.Include(i => i.category);
            if(sortby==2)
            {
                items= items.OrderBy(u => u.Title);

            }
            if (sortby == 3)
            {
                items = items.OrderBy(u => u.Price);

            }
            if (sortby == 4)
            {
                items = items.OrderBy(u => u.AddedOn);

            }
            

            return View(items.ToList());
            
        }



        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

       

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Genre");
            return View();
        }
        public ActionResult OutofStock()
        {
            ViewBag.Message = "The Item you selected is not available now.";
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item, HttpPostedFileBase image, HttpPostedFileBase audio)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);

                    fileName = DateTime.Now.Ticks + fileName;
                    WebImage img = new WebImage(image.InputStream);
                    if (img.Width > 100)
                    {
                        img.Resize(100, img.Height);
                    }

                    if (img.Height > 190)
                    {
                        img.Resize(img.Width, 190);
                    }

                    img.Save(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                    item.CoverPic = "/Uploads/" + fileName;
                }
                if (AudioUploadValidator.IsWebFriendlyAudio(audio))
                {
                    var fileName = Path.GetFileName(audio.FileName);

                    

                    audio.SaveAs(Path.Combine(Server.MapPath("~/Audio/"), fileName));
                    item.FilePath = "/Audio/" + fileName;
                    item.FileSize = audio.ContentLength;
                    item.AddedOn = DateTime.Now;
                    //item.Duration = audioHelper.GetAudioDuration(Path.GetFullPath(item.FilePath));
                }
                else
                {
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Genre", item.CategoryId);
                    return View(item);
                }
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("GetDuration",new {id=item.Id});
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Genre", item.CategoryId);
            return View(item);
        }
        //public ActionResult GetDuration(int? id)
        //{
        //    var item = db.Items.Find(id);
        //    item.Duration = audioHelper.GetAudioDuration(Path.GetFullPath(item.FilePath));
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Genre", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,OrderId,CategoryId,AddedOn,FileSize,FilePath,CoverPic,Price,Qty")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Genre", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
