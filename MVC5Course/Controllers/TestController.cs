using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
        public FabricsEntities db = new FabricsEntities();


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EDE()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EDE(EDEViewModel data)
        {
            return View(data);
        }

        public ActionResult CreateProduct()
        {
            var product = new Product()
            {
                ProductName = "Tercel",
                Active = true,
                Price = 1999,
                Stock = 5
            };
            db.Products.Add(product);
            db.SaveChanges();

            

            return View(product);

        }

        public ActionResult ReadProdcut(bool? Active)
        {
            var data = db.Products.AsQueryable();

            data = data.Where(p => p.ProductId > 1550).OrderByDescending(p => p.Price);

            if (Active.HasValue)
            {
                data = data.Where(p => p.Active == Active);
            }

            //var data = db.Products.whe
            return View(data);

        }

        public ActionResult OneProduct(int id )

        {
            var data = db.Products.Find(id);

            return View(data);
        }

        public ActionResult UpdateProduct(int id)
        {
            var one = db.Products.FirstOrDefault(p => p.ProductId == id);

            if (one == null)
            {
                return HttpNotFound();
            }

            one.Price = one.Price * 2;
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityError in ex.EntityValidationErrors)
                {
                    foreach (var err   in entityError.ValidationErrors)
                    {
                        return Content(err.PropertyName + ":" + err.ErrorMessage);
                    }

                }

                throw;
            }
            return RedirectToAction("ReadProdcut");
          

        }

        public ActionResult  DeleteProduct(int id)
        {
            var one = db.Products.Find(id);
            //foreach (var item in one.OrderLines.ToList())
            //{
            //    db.OrderLines.Remove(item);
            //}
            db.OrderLines.RemoveRange(one.OrderLines);
            
            //db.Database.ExecuteSqlCommand(@"Delete From dbo.OrderLine where ProductId=@p0")

            db.Products.Remove(one);
            db.SaveChanges();
            return RedirectToAction("ReadProdcut");
        }

        public ActionResult ProductView()
        {
            var data = db.Database.SqlQuery<ProductViewModel>(
                 @"SELECT * FROM dbo.Product WHERE Active = @p0 AND
                    ProductName like @p1", true, "%Yellow%");

            return View(data);

        }
      
        public ActionResult ProductSP()
        {
            var data = db.GetProduct(true, "%Yellow%");

            return View(data);
        }


    }
}