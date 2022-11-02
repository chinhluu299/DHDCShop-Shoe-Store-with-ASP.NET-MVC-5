using DHDCShop.Common;
using DHDCShop.Models;
using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        DHDCShopDbContext db = new DHDCShopDbContext();

        // GET: Admin/Product

        public ActionResult Index()
        {
            
            return View(db.Products.ToList());
         

        }

        // GET: Admin/Product/Details/5
        //public ActionResult Details(int id)
        //{
        //    if (Session["username"] != null)
        //    {
        //        return View(db.GIAYs.Find(id));
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }


        //}

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
             return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product upd, HttpPostedFileBase[] files, FormCollection form)
        {
            try
            {
                // TODO: Add insert logic here
                Product product = new Product();
                int count_size = 0;
                if (form["size_1"].Length > 0 && form["quantity_1"].Length > 0)
                {
                    count_size = 1;
                }



                if (upd.Name == null)
                {
                    ViewBag.Error = "Please input name of shoe";
                }
                else if (count_size < 1)
                {
                    ViewBag.Error = "Please input size of shoe";
                }
                else if (upd.Price.ToString().IsEmpty())
                {
                    ViewBag.Error = "Please input price of shoe";
                }
                else
                {
                    product.Gender = upd.Gender;
                    product.Name = upd.Name;
                    //giay.soluong = upd.soluong;
                    product.Brand = upd.Brand;
                    product.Description = upd.Description;
                    product.Price = upd.Price;
                    product.Rating = 0;
                    product.NumOfVote = 0;
                    product.Quantity = 0;

                    db.Products.Add(product);
                    db.SaveChanges();


                    var getProduct = db.Products.Select(item => item).OrderByDescending(item => item.ProductId).FirstOrDefault();
                    int id = getProduct.ProductId;

                    if (files != null)
                    {
                        var file = files[0];
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var ext = Path.GetExtension(file.FileName);
                            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                            string myfile = "shoe_" + id + ext; //appending the name with id  
                                                                // store the file inside ~/project folder(Img)  
                            var path = "~/Source/" + myfile;
                            var path2 = Path.Combine(Server.MapPath("~/Source"), myfile);
                            product.ImagePath = path;
                            file.SaveAs(path2);

                            foreach (var image in files)
                            {
                                var f_name = Path.GetFileName(image.FileName);
                                var f_ext = Path.GetExtension(image.FileName);
                                string f_nameWe = Path.GetFileNameWithoutExtension(f_name);
                                string f_myfile = "shoe_image_" + id + f_nameWe + f_ext;

                                var f_path = "~/Source/Product/" + f_myfile;
                                var f_path2 = Path.Combine(Server.MapPath("~/Source/Product"), f_myfile);
                                ProductImage pro_image = new ProductImage();
                                pro_image.ImagePath = f_path;
                                image.SaveAs(f_path2);
                                getProduct.ProductImages.Add(pro_image);

                            }
                        }
                    }



                    getProduct.Quantity = 0;

                    while (form["size_" + count_size] != null)
                    {
                        if (form["size_" + count_size].Length > 0 && form["quantity_" + count_size].Length > 0)
                        {
                            ProductSize size = new ProductSize();
                            size.ProductId = id;
                            size.Size = int.Parse(form["size_" + count_size]);
                            size.Quantity = int.Parse(form["quantity_" + count_size]);
                            getProduct.Quantity += size.Quantity;

                            db.ProductSizes.Add(size);
                        }
                        count_size++;

                    }
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (db.Products.Find(id) != null)
            {
                return View(db.Products.Find(id));
            }
            return RedirectToAction("Index");

        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Product upd, HttpPostedFileBase[] files, FormCollection form)
        {
            try
            {
                // TODO: Add update logic here
                //GIAY giay = db.GIAYs.Find(id);

                //giay.gioitinh = collection["gioitinh"];
                //giay.tengiay = collection["tengiay"];
                //giay.soluong = int.Parse(collection["soluong"]);
                //giay.hang = collection["hang"];
                //giay.size = int.Parse(collection["size"]);
                //giay.chitiet = collection["chitiet"];
                //giay.gia = decimal.Parse(collection["gia"]);
                int count_size = 0;
                if (form["size_1"].Length > 0 && form["quantity_1"].Length > 0)
                {
                    count_size = 1;
                }

                if (upd.Name == null)
                {
                    ViewBag.Error = "Please input name of shoe";
                }
                else if (count_size < 1)
                {
                    ViewBag.Error = "Please input size of shoe";
                }
                else if (upd.Price.ToString().IsEmpty())
                {
                    ViewBag.Error = "Please input price of shoe";
                }
                else
                {
                    var products = db.Products.Where(s => s.ProductId == upd.ProductId).ToList();
                    foreach (var item in products)
                    {
                        int id = item.ProductId;
                        item.Gender = upd.Gender;
                        item.Name = upd.Name;
                        item.Quantity = upd.Quantity;
                        item.Brand = upd.Brand;
                        item.Price = upd.Price;

                        //add chitiet
                        var descriptionEncode = upd.Description;
                        item.Description = descriptionEncode;

                        if (files != null)
                        {
                            //save in product
                            var file = files[0];
                            if (file != null)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var ext = Path.GetExtension(file.FileName);
                                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                string myfile = "shoe_" + id + ext; //appending the name with id  
                                                                    // store the file inside ~/project folder(Img)  
                                var path = "~/Source/" + myfile;
                                var path2 = Path.Combine(Server.MapPath("~/Source"), myfile);


                                var path_del = Server.MapPath(item.ImagePath);
                                FileInfo file2 = new FileInfo(path_del);
                                if (file2.Exists)//check file exsit or not  
                                {
                                    file2.Delete();
                                }
                                item.ImagePath = path;
                                file.SaveAs(path2);

                                //save in iamges
                                var data_image = item.ProductImages;
                                foreach (var image in data_image)
                                {
                                    var del = Server.MapPath(image.ImagePath);
                                    FileInfo imageDel = new FileInfo(del);
                                    if (imageDel.Exists)
                                    {
                                        imageDel.Delete();
                                    }
                                }
                                item.ProductImages.Clear();
                                db.ProductImages.RemoveRange(db.ProductImages.Where(s => s.ProductId == item.ProductId));

                                foreach (var image in files)
                                {
                                    var f_name = Path.GetFileName(image.FileName);
                                    var f_ext = Path.GetExtension(image.FileName);
                                    string f_nameWe = Path.GetFileNameWithoutExtension(f_name);
                                    string f_myfile = "shoe_image_" + id + f_nameWe + f_ext;

                                    var f_path = "~/Source/Product/" + f_myfile;
                                    var f_path2 = Path.Combine(Server.MapPath("~/Source/Product"), f_myfile);
                                    ProductImage pro_image = new ProductImage();
                                    pro_image.ImagePath = f_path;
                                    image.SaveAs(f_path2);
                                    item.ProductImages.Add(pro_image);

                                }
                            }
                        }
                    }
                }
                db.SaveChanges();

                Product product = db.Products.Find(upd.ProductId);

                db.ProductSizes.RemoveRange(product.ProductSizes);

                db.SaveChanges();
                product.Quantity = 0;

                while (form["size_" + count_size] != null)
                {
                    if (form["size_" + count_size].Length > 0 && form["quantity_" + count_size].Length > 0)
                    {
                        ProductSize size = new ProductSize();
                        size.ProductId = upd.ProductId;
                        size.Size = int.Parse(form["size_" + count_size]);
                        size.Quantity = int.Parse(form["quantity_" + count_size]);
                        product.Quantity += size.Quantity;

                        db.ProductSizes.Add(size);
                    }
                    count_size++;

                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Product/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View(db.Products.Find(id));
        //}

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = db.Products.Find(id);

                var path = Server.MapPath(product.ImagePath);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }

                foreach (var item in product.ProductImages.ToList())
                {
                    var path_item = Server.MapPath(item.ImagePath);
                    FileInfo file_item = new FileInfo(path_item);
                    if (file_item.Exists)
                    {
                        file_item.Delete();
                    }
                    db.ProductImages.Remove(item);
                }

                db.ProductSizes.RemoveRange(product.ProductSizes);
                db.Comments.RemoveRange(product.Comments);
                db.OrderDetails.RemoveRange(product.OrderDetails);


                db.Products.Remove(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}