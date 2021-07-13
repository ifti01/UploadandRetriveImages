using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadRetriveImages.Models;

namespace UploadRetriveImages.Controllers
{
    public class HomeController : Controller
    {
        SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            string fileName = Path.GetFileNameWithoutExtension(student.ImageFile.FileName);
            string extension = Path.GetExtension(student.ImageFile.FileName);
            HttpPostedFileBase postedFile = student.ImageFile;
            int length = postedFile.ContentLength;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (length <= 1024 * 1024 * 2)
                {
                    fileName = fileName + extension;
                    student.Imagepath = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                    student.ImageFile.SaveAs(fileName);
                    db.Students.Add(student);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["CreateMessage"] = "<script>alert('DATA INSERTED')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["CreateMessage"] = "<script>alert('INSERTION FAILED')</script>";

                    }
                }
                else
                {
                    TempData["SizeMessage"] = "<script>alert('Image Size should be less than 2 MB')</script>";
                }
            }
            else
            {
                TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
            }

            return View();
        }
        
        public ActionResult Edit(int id)
        {
            var EmployeeRow = db.Students.Where(model => model.ID == id).FirstOrDefault();
            Session["Image"] = EmployeeRow.Imagepath;
            return View(EmployeeRow);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid == true)
            {
                if (student.ImageFile!=null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(student.ImageFile.FileName);
                    string extension = Path.GetExtension(student.ImageFile.FileName);
                    HttpPostedFileBase postedFile = student.ImageFile;
                    int length = postedFile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1024 * 1024 * 2)
                        {
                            fileName = fileName + extension;
                            student.Imagepath = "~/images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                            student.ImageFile.SaveAs(fileName);
                            db.Entry(student).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                string imagePath = Request.MapPath(Session["Image"].ToString());
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }

                                TempData["UPDATEMessage"] = "<script>alert('DATA UPDATED SUCCESSFULLY')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["UPDATEMessage"] = "<script>alert('NOT UPDATED!!')</script>";

                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size should be less than 2 MB')</script>";
                        }
                    }

                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    student.Imagepath = Session["Image"].ToString();
                    db.Entry(student).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UPDATEMessage"] = "<script>alert('DATA updated SUCCESSFULLY')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["UPDATEMessage"] = "<script>alert('NOT UPDATED!!')</script>";

                    }
                }
            }

            return View();
            
        }

        public ActionResult Delete(int id)
        {
            if (id>0)
            {
                var EmployeeRow = db.Students.Where(model => model.ID == id).FirstOrDefault();

                if (EmployeeRow!= null)
                {
                    db.Entry(EmployeeRow).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a>0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Deleted')</script>";
                        string imagePath = Request.MapPath(EmployeeRow.Imagepath.ToString());
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    else
                    {

                        TempData["DeleteMessage"] = "<script>alert('Data Not Deleted')</script>";
                    }
                }

            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Details(int id)
        {
            var EmployeeRow = db.Students.Where(model => model.ID == id).FirstOrDefault();
            Session["Image2"] = EmployeeRow.Imagepath.ToString();
            return View(EmployeeRow);
        }
    }
}
