/*Controller for admin*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Data;
using WebApplication2.Models;
/*using System.Web.Mvc;*/
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using Microsoft.Graph.Models.TermStore;
using Microsoft.Graph.Models;
using WebApplication2.Models.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Http.Metadata;
using System.Xml.Linq;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult MetadataFormCreate()
        {
            return View();
        }


        public ActionResult AdminIndex()
        {

            return View(db.DocumentType.ToList());
        }


        // GET: Admin/CreateContentType
        public ActionResult CreateContentType()
        {
            /*var documentTypes = GetDocumentTypesFromDatabase();
            var documentTypeList = new SelectList(documentTypes, "DocumentType");
            var model = new Metadata();
            model.DocumentType = "Letter";
            ViewBag.DocumentTypeList = documentTypeList;*/

            /*var model = new Metadata
            {
                DocumentType = db.MetadataForms.ToList()
            };*/
            /*return View(model);*/

            var contentType = db.ContentTypes.ToList();

            if (contentType != null && contentType.Any())
            {
                ViewBag.ContentType = contentType;
            }
            else
            {
                ViewBag.ContentType = new List<Models.ContentType>(); // or an empty list
            }
            return View();
        }

        // POST: Admin/CreateContentType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContentType(DocumentTypeOption option)
        {
            if (ModelState.IsValid)
            {
                option.CreatedByEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                db.DocumentType.Add(option);
                db.SaveChanges();

                // Log the change
                /*var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Content Type Option added",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = option.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = "-",
                    NewDocTypeOption = option.DocumentTypeOptionValue
                };
                db.DocTypeLog.Add(auditLog);
                db.SaveChanges();*/

                return RedirectToAction("CreateContentType");
            }
            return View(option);
        }

        // GET: Admin/CreateFolder
        /*public IActionResult CreateFolder()
        {
            var parentFolder = db.DocFolder.ToList();

            if(parentFolder != null && parentFolder.Any())
            {
                ViewBag.ParentFolder = parentFolder;
            }
            else
            {
                ViewBag.ParentFolder = new List<DocumentFolder>();
            }
            return View();
        }*/
        public IActionResult CreateFolder()
        {
            // Get the list of parent folders from the database
            var parentFolders = db.DocFolder.Select(f => f.FolderName).ToList();

            // Create a SelectList from the list of parent folders
            var options = new SelectList(parentFolders);

            // Populate the ViewBag.Options
            ViewBag.Options = options;

            return View();
        }




        /*// POST: Admin/CreateFolder
        [HttpPost]
        public IActionResult CreateFolder([Bind("FolderName,FolderPath,CreatedBy,CreatedByEmail")] DocumentFolder documentFolder)
        {
            if (ModelState.IsValid)
            {
                List<DocumentFolder> folders = new List<DocumentFolder>();
                folders.Add(documentFolder);
                ViewBag.FolderName = folders;
                return View("Create", documentFolder);
            }
            return View(documentFolder);
        }*/

        /*[HttpPost]
        public ActionResult CreateFolder(string folderName)
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                *//*string folderPath = HttpContext.Current.Server.MapPath("~/UserFolders/NewFolder");*/
        /*string folderPath = Server.MapPath($"~/UserFolders/NewFolder");*//*
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFolder", folderName);
        *//*string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "UserFolders", folderName);*//*
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            ViewBag.Message = "Folder created successfully.";
        }
        else
        {
            ViewBag.Message = "Folder already exists.";
        }
    }
    else
    {
        ViewBag.Message = "Folder name cannot be empty.";
    }
    return View();
}*/

        /*[HttpPost]
        public IActionResult CreateFolder(string folderName, string parentFolder)
        {
            // validate input
            if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(parentFolder))
            {
                return BadRequest("Folder name and parent folder name are required");
            }

            // get the path to the parent folder
            string parentFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", parentFolder);

            // create the folder
            string folderPath = Path.Combine(parentFolderPath, folderName);
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                return BadRequest("Folder already exists");
            }

            // store folder metadata in database
                db.DocFolder.Add(new DocumentFolder { FolderName = folderName, ParentFolder = parentFolder });
                db.SaveChanges();
             

            return Ok("Folder created successfully");
        }*/

        [HttpPost]
        public IActionResult CreateFolder(DocumentFolder model)
        {
            // validate input
            if (ModelState.IsValid)
            {
                // get the path to the parent folder
                string parentFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", model.ParentFolder);

                // create the folder
                string folderPath = Path.Combine(parentFolderPath, model.FolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                else
                {
                    ModelState.AddModelError("", "Folder already exists");
                    return View(model);
                }

                // store folder metadata in database
                db.DocFolder.Add(model);
                db.SaveChanges();

                return RedirectToAction("CreateFolder");
            }

            // Get the list of parent folders from the database
            var parentFolders = db.DocFolder.Select(f => f.FolderName).ToList();

            // Create a SelectList from the list of parent folders
            var options = new SelectList(parentFolders);

            // Populate the ViewBag.Options
            ViewBag.Options = options;

            return View(model);
        }


        // GET: Admin/CreateDocumentType
        public ActionResult CreateDocumentType()
        {
            /*var documentTypes = GetDocumentTypesFromDatabase();
            var documentTypeList = new SelectList(documentTypes, "DocumentType");
            var model = new Metadata();
            model.DocumentType = "Letter";
            ViewBag.DocumentTypeList = documentTypeList;*/

            /*var model = new Metadata
            {
                DocumentType = db.MetadataForms.ToList()
            };*/
            /*return View(model);*/

            var documentType = db.DocumentType.ToList();

            if (documentType != null && documentType.Any())
            {
                ViewBag.DocumentTypeOption = documentType;
            }
            else
            {
                ViewBag.DocumentTypeOption = new List<DocumentTypeOption>(); // or an empty list
            }
            return View();
        }

        /*private IEnumerable GetCategoriesFromDatabase()
        {
            throw new NotImplementedException();
        }*/

        // POST: Admin/CreateDocumentType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDocumentType(DocumentTypeOption option)
        {
            if (ModelState.IsValid)
            {
                option.CreatedByEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                db.DocumentType.Add(option);
                db.SaveChanges();

                // Log the change
                var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Document Type Option added",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = option.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = "-",
                    NewDocTypeOption = option.DocumentTypeOptionValue
                };
                db.DocTypeLog.Add(auditLog);
                db.SaveChanges();

                return RedirectToAction("CreateDocumentType");
            }
            return View(option);
        }


        // GET: Admin/CreateMetadata
        /*public ActionResult CreateMetadata()
        {
            *//*var documentTypes = GetDocumentTypesFromDatabase();
            var documentTypeList = new SelectList(documentTypes, "DocumentType");
            var model = new Metadata();
            model.DocumentType = "Letter";
            ViewBag.DocumentTypeList = documentTypeList;*/

            /*var model = new Metadata
            {
                DocumentType = db.MetadataForms.ToList()
            };*/
            /*return View(model);*//*

            var siteName = db.DocumentType.ToList();

            if (documentType != null && documentType.Any())
            {
                ViewBag.DocumentTypeOption = documentType;
            }
            else
            {
                ViewBag.DocumentTypeOption = new List<DocumentTypeOption>(); // or an empty list
            }
            return View();
        }*/


        protected override void Dispose(bool disposing)
        {
            if (disposing && (db != null))
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Admin/EditDocumentType
        [HttpGet]
        public async Task<IActionResult> EditDocumentType(int Id)
        {
            var doctype = await db.DocumentType.FindAsync(Id);
            if (doctype == null)
            {
                return NotFound();
            }

            var viewModel = new DocumentTypeOption
            {
                DocumentTypeOptionId = doctype.DocumentTypeOptionId,
                DocumentTypeOptionValue = doctype.DocumentTypeOptionValue
            };
            return View("EditDocumentType", viewModel);
        }

        // POST: Admin/EditDocumentType
        [HttpPost]
        public async Task<IActionResult> EditDocumentType(DocumentTypeOption viewModel)
        {
            if (ModelState.IsValid)
            {
                var doctype = await db.DocumentType.FindAsync(viewModel.DocumentTypeOptionId);
                if (doctype == null)
                {
                    return NotFound();
                }

                var currentDocumentTypeOption = doctype.DocumentTypeOptionValue;
                doctype.DocumentTypeOptionValue = viewModel.DocumentTypeOptionValue;

                db.DocumentType.Update(doctype);
                await db.SaveChangesAsync();

                // Log the change
                var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Document Type Option updated",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = doctype.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = currentDocumentTypeOption,
                    NewDocTypeOption = doctype.DocumentTypeOptionValue
                };
                db.DocTypeLog.Add(auditLog);
                await db.SaveChangesAsync();

                /*return RedirectToAction(nameof(MetadataFormDropDown));*/
                return Json(new { success = true });
            }
            /*return View("EditDocumentType", viewModel);*/
            else
            {
                return Json(new { success = false, error = "Document type update failed." });
            }
        }

        // POST: Delete Option
        [HttpPost]
        public async Task<IActionResult> DeleteDocumentType(int Id)
        {
            var option = await db.DocumentType.FindAsync(Id);
            if (option != null)
            {
                // Log the changes
                var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Document Type Option deleted",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = option.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = option.DocumentTypeOptionValue,
                    NewDocTypeOption = "-"
                };

                db.DocTypeLog.Add(auditLog);
                await db.SaveChangesAsync();

                db.DocumentType.Remove(option);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Document type option failed to be deleted." });
        }

        // GET: Admin/DocumentTypeLogTable
        public async Task<IActionResult> DocumentTypeLogTable()
        {
            var auditLogs = await db.DocTypeLog.ToListAsync();
            return View(auditLogs);
        }

        // GET: Home/CreateDivision (Use this)
        public ActionResult CreateDivision()
        {
            var divisionName = db.Divisions.ToList();

            if (divisionName != null && divisionName.Any())
            {
                ViewBag.DivisionName = divisionName;
            }
            else
            {
                ViewBag.DivisionName = new List<Division>();
            }

            return View();
        }

        /*// POST: Home/CreateSite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSite(SiteLocation site)
        {
            *//*if(viewModel.SiteName != null && viewModel.DepartmentName != null)
            {
                var site = new Models.Site
                {
                    SiteName = viewModel.SiteName
                };

                var department = new Department
                {
                    DepartmentName = viewModel.DepartmentName
                };

                await dbContext.Sites.AddAsync(site);
                await dbContext.Departments.AddAsync(department);
                await dbContext.SaveChangesAsync();

                return Json(new { suceess = true });
            }
            else
            {
                return Json(new { success = false, error = "Failed to create new site or department." });
            }*/

            // POST: Home/CreateSite (Use this)
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateDivision(Division division)
            {
                if (ModelState.IsValid)
            {
                division.CreatedByEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                db.Divisions.Add(division);
                db.SaveChanges();

                return RedirectToAction("CreateDivision");
                /*return Json(new { success = true });*/
            }
            return View(division);
            /*else
            {
                return Json(new { success = false, error = "Model state is invalid" });
            }*/
        }

        // GET: Home/CreateDepartment
        /*public ActionResult CreateDepartment()
        {
           *//* ViewBag.SiteName = new SelectList(db.Sites, "SiteName", "SiteName");*//*

            var departmentName = db.Departments.ToList();
            *//*var siteName = db.Sites.ToList();*//*

            if (departmentName != null && departmentName.Any())
            {

                *//*ViewBag.DepartmentName = departmentName.Select(s => new SelectListItem { Value = s.DepartmentId.ToString(), Text = s.DepartmentName });*//*
                ViewBag.DepartmentName = departmentName;
            }
            else
            {
                *//*ViewBag.SiteName = new SelectList(db.Sites, "SiteName", "SiteName");*//*
                ViewBag.DepartmentName = new List<Department>();
            }

            *//*ViewBag.SiteName = new SelectList(db.Sites, "SiteId", "SiteName");*//*
            return View();
        }*/

        // CreateSite (2nd set) - start
        // GET: CreateSite
        /*public IActionResult CreateSite()
        {
            return View(db.Sites.ToList());
        }

        // GET: CreateSite/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = db.Sites
                .Include(s => s.Departments)
                .FirstOrDefault(m => m.SiteId == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: CreateSite/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreateSite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,SiteName,CreatedByEmail,CreatedDate")] SiteLocation site)
        {
            Console.WriteLine("Create action hit");
            if (ModelState.IsValid)
            {
                *//*db.Add(site);
                db.SaveChanges();*//*
                db.Add(site);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(CreateSite));
            }
            return View(site);
        }

        // GET: CreateSite/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = db.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }
            return View(site);
        }

        // POST: CreateSite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SiteLocation site)
        {
            if (id != site.SiteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(site);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteLocationExists(site.SiteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CreateSite));
            }
            return View(site);
        }

        // GET: CreateSite/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = db.Sites
                .Include(s => s.Departments)
                .FirstOrDefault(m => m.SiteId == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: CreateSite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var site = db.Sites.Find(id);
            db.Sites.Remove(site);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteLocationExists(int id)
        {
            return db.Sites.Any(e => e.SiteId == id);
        }*/
        // CreateSite(2nd set) - end

        public ActionResult CreateDepartment()
        {
            var department = db.Departments.ToList();
            if (department != null && department.Any())
            {
                ViewBag.Departments = department;
            }
            else
            {
                ViewBag.DocumentTypeOption = new List<DocumentTypeOption>(); // or an empty list
            }

            ViewBag.DivisionName = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            return View();
        }



        // POST: Home/CreateDepartment
        /*[HttpPost]
        public ActionResult CreateDepartment(Department option)
        {


            if (ModelState.IsValid)
            {
                *//*var siteName = db.Sites.ToList();
                ViewBag.SiteName = new SelectList(siteName, "SiteId", "SiteName");*//*

                option.CreatedByEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "Unknown";
                db.Departments.Add(option);
                db.SaveChanges();

                return RedirectToAction("CreateDepartment");
                *//*return Json(new { success = true });*//*
            }



            // Repopulate the ViewBag here, otherwise the dropdown lists will be empty
           *//* var siteName = db.Sites.ToList();
            var departmentName = db.Departments.ToList();*/

        /*ViewBag.SiteName = new SelectList(siteName, "SiteId", "SiteName");
        ViewBag.DepartmentName = new SelectList(departmentName, "DepartmentId", "DepartmentName");*//*

        return View(option);
    }*/

        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                department.CreatedByEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "Unknown";
                /*department.SiteName = db.Sites.Find(department.SiteId).SiteName;*/ // Get the site name from the database
                db.Departments.Add(department);
                db.SaveChanges();

                // Return a list of departments to display in the view
                /*ViewBag.DepartmentName = db.Departments.ToList();*/

                return RedirectToAction("CreateDepartment");
            }

            ViewBag.DivisionName = new SelectList(db.Divisions, "DivisionId", "DivisionName", department.DivisionId);
            return View(department);
        }


        /* [HttpPost]
         public async Task<IActionResult> CreateDepartment(Department option)
         {


             if (ModelState.IsValid)
             {
                 var userClaims = User.Identity as ClaimsIdentity;
                 var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";


                 var dept = new Department
                 {
                     DepartmentName = option.DepartmentName,
                     SiteDepartmentName = option.SiteDepartmentName,
                     CreatedDate = ConvertToMalaysianTime(DateTime.UtcNow),
                     CreatedByEmail = userEmail
                 };

                 await db.Departments.AddAsync(dept);
                 await db.SaveChangesAsync();

                 *//*return RedirectToAction("CreateDepartment");*//*
                 return Json(new { success = true });
             }

             else
             {
                 return Json(new { success = false, error = "Model not valid." });
             }
         }*/


        /*[HttpPost]
        public ActionResult CreateDepartment(DepartmentVM option)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    DepartmentName = option.DepartmentName,
                    SiteDepartmentId = option.SiteDepartmentId
                };

                db.Departments.Add(department);
                db.SaveChanges();

                return RedirectToAction("CreateDepartment");
            }

            var siteName = db.Sites.ToList();
            var departmentName = db.Departments.ToList();

            ViewBag.SiteName = new SelectList(siteName, "SiteId", "SiteName");
            ViewBag.DepartmentName = new SelectList(departmentName, "DepartmentId", "DepartmentName");

            return View(option);
        }*/


        /*// GET: Admin/CreateDepartment
        public IActionResult CreateDepartment()
        {
            ViewData["SiteId"] = new SelectList(db.Sites, "SiteId", "SiteName");
            return View();
        }

        // POST: Admin/CreateDepartment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment([Bind("DepartmentName,SiteId")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SiteId"] = new SelectList(db.Sites, "SiteId", "SiteName");
            return View(department);
        }*/

        private DateTime ConvertToMalaysianTime(DateTime utcDateTime)
        {
            try
            {
                TimeZoneInfo malaysiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
                DateTime malaysiaTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, malaysiaTimeZone);
                return malaysiaTime;
            }
            catch (TimeZoneNotFoundException)
            {
                // Handle exception if the time zone identifier is not found
                throw new Exception("The Malaysia Standard Time zone is not found on the local system.");
            }
            catch (InvalidTimeZoneException)
            {
                // Handle exception if the time zone identifier is invalid
                throw new Exception("The Malaysia Standard Time zone is invalid.");

                /*   if{ 
                   throw new Exception("the standard malaysia time is not valid ");
                       el*/
            }
        }

        // GET: Admin/EditDivision
        [HttpGet]
        public async Task<IActionResult> EditDivision(int Id)
        {
            var divisionName = await db.Divisions.FindAsync(Id);
            if (divisionName == null)
            {
                return NotFound();
            }

            var viewModel = new Division
            {
                DivisionId = divisionName.DivisionId,
                DivisionName = divisionName.DivisionName
            };
            return View("EditDivision", viewModel);
        }

        // POST: Admin/EditDocumentType
        [HttpPost]
        public async Task<IActionResult> EditDivision(Division viewModel)
        {
            if (ModelState.IsValid)
            {
                var divisionName = await db.Divisions.FindAsync(viewModel.DivisionId);
                if (divisionName == null)
                {
                    return NotFound();
                }

                var currentDivisionName = divisionName.DivisionName;
                divisionName.DivisionName = viewModel.DivisionName;

                db.Divisions.Update(divisionName);
                await db.SaveChangesAsync();

                // Log the change
                /*var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Document Type Option updated",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = doctype.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = currentDocumentTypeOption,
                    NewDocTypeOption = doctype.DocumentTypeOptionValue
                };
                db.DocTypeLog.Add(auditLog);
                await db.SaveChangesAsync();*/

                /*return RedirectToAction(nameof(MetadataFormDropDown));*/
                return Json(new { success = true });
            }
            /*return View("EditDocumentType", viewModel);*/
            else
            {
                return Json(new { success = false, error = "Site update failed." });
            }
        }

        // POST: Delete Option
        [HttpPost]
        public async Task<IActionResult> DeleteSite(int Id)
        {
            var option = await db.Divisions.FindAsync(Id);
            if (option != null)
            {
                // Log the changes
                /*var auditLog = new DocumentTypeLog
                {
                    DocTypeLogDate = DateTime.Now,
                    DocTypeLogActivity = "Document Type Option deleted",
                    DocTypeLogEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    DocTypeOptionId = option.DocumentTypeOptionId.ToString(),
                    CurrentDocTypeOption = option.DocumentTypeOptionValue,
                    NewDocTypeOption = "-"
                };

                db.DocTypeLog.Add(auditLog);
                await db.SaveChangesAsync();*/

                db.Divisions.Remove(option);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Site failed to be deleted." });
        }

        private bool OptionExists(int id)
        {
            return db.DocumentType.Any(e => e.DocumentTypeOptionId == id);
        }

    }
}