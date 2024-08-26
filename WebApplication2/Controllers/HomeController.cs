/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.Entities;
using System;

namespace WebApplication2.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IWebHostEnvironment _webHost;

    public HomeController(IWebHostEnvironment webHost, ApplicationDbContext dbContext)
    {
      _webHost = webHost;
      this.dbContext = dbContext;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    public IActionResult Login()
    {
      return View();
    }

    public IActionResult Approver()
    {
      var items = dbContext.MetadataForms.ToList();
      return View(items);
    }

    public IActionResult DocumentManagement()
    {
      var items = dbContext.MetadataForms.ToList(); // Fetch metadata entries from the database
      return View(items); // Pass the metadata entries to the view
    }

    // Add this action to display the metadata form
    public IActionResult Metadata()
    {
      return View(new AddMetadata());
    }

    [HttpPost]
    public async Task<IActionResult> Metadata(AddMetadata viewModel)
    {
      if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
      {
        using (var memoryStream = new MemoryStream())
        {
          await viewModel.DocumentFile.CopyToAsync(memoryStream);
          var metadata = new Metadata
          {
            DocumentName = viewModel.DocumentName,
            DocumentType = viewModel.DocumentType,
            DocumentFormat = viewModel.DocumentFormat,
            DocumentTest = viewModel.DocumentTest,
            DocumentContent = memoryStream.ToArray(),
            Status = "Pending" // default status
          };

          await dbContext.MetadataForms.AddAsync(metadata);
          await dbContext.SaveChangesAsync();
          return Json(new { success = true }); // Added this to return success response
        }
      }
      else
      {
        return Json(new { success = false, error = "File upload failed." });
      }
    }

    // Add this action to handle deletion of metadata entries
   
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
      var metadata = await dbContext.MetadataForms.FindAsync(id);
      if (metadata != null)
      {
        dbContext.MetadataForms.Remove(metadata);
        await dbContext.SaveChangesAsync();
        return Json(new { success = true, message = "Metadata deleted successfully." });
      }
      return Json(new { success = false, error = "Deletion failed." });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(Guid id, string status)
    {
      var metadata = await dbContext.MetadataForms.FindAsync(id);
      if (metadata != null)
      {
        metadata.Status = status;
        dbContext.MetadataForms.Update(metadata);
        await dbContext.SaveChangesAsync();
        return Json(new { success = true, message = $"Document {status.ToLower()} successfully." });
      }
      return Json(new { success = false, error = "Update failed." });
    }
*/


/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.Entities;
using System;

namespace WebApplication2.Controllers
{
  [Authorize] // Add this to protect the entire controller
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IWebHostEnvironment _webHost;

    public HomeController(IWebHostEnvironment webHost, ApplicationDbContext dbContext)
    {
      _webHost = webHost;
      this.dbContext = dbContext;
    }

    public IActionResult Index()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
      return View();
    }

    public IActionResult DocumentManagement()
    {
      var items = dbContext.MetadataForms.ToList(); // Fetch metadata entries from the database
      return View(items); // Pass the metadata entries to the view
    }

    // Add this action to display the metadata form
    public IActionResult Metadata()
    {
      return View(new AddMetadata());
    }

    [HttpPost]
    public async Task<IActionResult> Metadata(AddMetadata viewModel)
    {
      if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
      {
        using (var memoryStream = new MemoryStream())
        {
          await viewModel.DocumentFile.CopyToAsync(memoryStream);
          var metadata = new Metadata
          {
            DocumentName = viewModel.DocumentName,
            DocumentType = viewModel.DocumentType,
            DocumentFormat = viewModel.DocumentFormat,
            DocumentTest = viewModel.DocumentTest,
            DocumentContent = memoryStream.ToArray()
          };

          await dbContext.MetadataForms.AddAsync(metadata);
          await dbContext.SaveChangesAsync();
          return Json(new { success = true }); // Added this to return success response
        }
      }
      else
      {
        return Json(new { success = false, error = "File upload failed." });
      }
    }

    // Add this action to handle deletion of metadata entries
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
      var metadata = await dbContext.MetadataForms.FindAsync(id);
      if (metadata != null)
      {
        dbContext.MetadataForms.Remove(metadata);
        await dbContext.SaveChangesAsync();
        return Json(new { success = true });
      }
      return Json(new { success = false, error = "Deletion failed." });
    }



*/



using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mime;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Worksheets.Item.Charts.ItemWithName;
using MyMvcProject.Helpers;
using Microsoft.Graph.Models;
using Microsoft.AspNetCore.Hosting.Server;
using System.Globalization;
using WebApplication2.Models.ViewModel;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment _webHost;

        // _rootPath
        private readonly string _rootPath;

        public HomeController(IWebHostEnvironment webHost, ApplicationDbContext dbContext)
        {
            _webHost = webHost;
            this.dbContext = dbContext;

            //_rootPath
            _rootPath = webHost.WebRootPath;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Approver()
        {
            var items = dbContext.MetadataForms.ToList();
            return View(items);
        }
        public IActionResult LogTable()
        {
            var items = dbContext.MetadataForms.ToList();
            return View(items);
        }

        public IActionResult DocumentManagement()
        {
            var items = dbContext.MetadataForms.ToList(); // Fetch metadata entries from the database
            return View(items); // Pass the metadata entries to the view
        }

        public ActionResult SearchDocument()
        {
            return View();
        }

        /*[HttpGet]
        public ActionResult SearchDocument(string documentName, string documentType)
        {
            var query = dbContext.MetadataForms.AsQueryable();

            if (!string.IsNullOrEmpty(documentName))
            {
                query = query.Where(d => d.DocumentName.Contains(documentName));
            }

            if (!string.IsNullOrEmpty(documentType))
            {
                query = query.Where(d => d.DocumentType == documentType);
            }

            var results = query.ToList();

            return View(results);
        }*/

        /*public ActionResult SiteOption()
        {
            var model = new Metadata();
            model.SiteName = new[]
            {
                new SelectListItem { Value = "1", Text = "The Ascent (WCT Land and Property)" },
                new SelectListItem { Value = "2", Text = "Damansara Town Center (WCT Engineering and Construction)" },
                new SelectListItem { Value = "3", Text = "Merdeka 118 (WCT Engineering and Construction)" }
            };
            return View(model);
        }*/

        [HttpGet]
        public ActionResult SearchDocument(string documentName, string documentType, string documentComment)
        {
            // Search logic
            var results = dbContext.MetadataForms
                .Where(x => x.DocumentName.Contains(documentName) && x.DocumentType.Contains(documentType))
                .ToList();

            return View(results);
        }

        public IActionResult EditForm()
        {
            ViewBag.Options = new SelectList(dbContext.DocumentType, "DocumentTypeOptionValue", "DocumentTypeOptionValue");
            return View(new AddMetadata());
        }

        public IActionResult ChangesTable()
        {
            return View();
        }


        public IActionResult DocumentTypeTable()
        {
            return View();
        }
        public IActionResult FileData()
        {
            return View();

        }




        // Add this action to display the metadata form
        public IActionResult Metadata()
        {
            ViewBag.Options = new SelectList(dbContext.DocumentType, "DocumentTypeOptionValue", "DocumentTypeOptionValue");
            ViewBag.Sites = new SelectList(dbContext.Divisions, "SiteName", "SiteName");

            /*return View(new AddMetadata());*/

            /*UserModel model = new UserModel();
            model.AddMetadata = new AddMetadata();
            return View(model);*/ //(use)

            var viewModel = new UserModel
            {
                Metadata = new Metadata(),
                AddMetadata = new AddMetadata(),
                LetterOfAppointment = new LetterOfAppointment()
            };
            return View(viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> Metadata(UserModel viewModel)
        {
            if (viewModel.AddMetadata.DocumentFile != null && viewModel.AddMetadata.DocumentFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.AddMetadata.DocumentFile.CopyToAsync(memoryStream);
                    // Extract user information from claims
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userName = userClaims?.FindFirst("name")?.Value ?? "unknown";
                    var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";

                    var metadata = new Metadata
                    {
                      
                        DocumentName = viewModel.AddMetadata.DocumentName,

                        SiteName = viewModel.AddMetadata.SiteName,
                        DepartmentName = viewModel.AddMetadata.DepartmentName,
                        MainFolderName = viewModel.AddMetadata.MainFolderName,
                        SubFolderName = viewModel.AddMetadata.SubFolderName,
                        SubDocumentName = viewModel.AddMetadata.SubDocumentName, 

                        DocumentType = viewModel.AddMetadata.DocumentType,
                        DocumentFormat = viewModel.AddMetadata.DocumentFormat,
                        DocumentDescription = viewModel.AddMetadata.DocumentDescription,
                        DocumentContent = memoryStream.ToArray(),
                        Status = "Pending", // default status
                        UploadDate = ConvertToMalaysianTime(DateTime.UtcNow),
                        CreatedBy = userName,
                        CreatedByEmail = userEmail,
                        OriginalExtension = viewModel.AddMetadata.OriginalExtension,

                        
                    };

                    var letterOfAppointment = new LetterOfAppointment
                    {

                        LoaReference = viewModel.LetterOfAppointment.LoaReference,
                        LoaProjectName = viewModel.LetterOfAppointment.LoaProjectName
                    };

                    await dbContext.MetadataForms.AddAsync(metadata);
                    await dbContext.LetterOfAppointments.AddAsync(letterOfAppointment);
                    await dbContext.SaveChangesAsync();
                    return Json(new { success = true }); // Added this to return success response
                }
            }
            else
            {
                return Json(new { success = false, error = "File upload failed." });
            }
        }

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

        [HttpGet]
        public IActionResult DownloadDocument(Guid id)
        {
            var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.DocumentId == id);
            if (metadata != null && metadata.DocumentContent != null && !string.IsNullOrEmpty(metadata.DocumentName))
            {
                // Mapping of file extensions to MIME types  
                var mimeTypes = new Dictionary<string, string>
                {
                    {".pdf", "application/pdf"},
                    {".doc", "application/msword"},
                    {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                    // Add more mappings as needed  
                };

                // Use the stored original extension
                string extension = metadata.OriginalExtension;

                // Get the MIME type  
                string mimeType = mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octet-stream";

                var contentDisposition = new ContentDisposition
                {
                    FileName = $"{Path.GetFileNameWithoutExtension(metadata.DocumentName)}{extension}",
                    Inline = false  // false = prompt the user for downloading; true = browser to try to show the file inline  
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(metadata.DocumentContent, mimeType);
            }
            return NotFound();
        }

        // Add this action to handle deletion of metadata entries
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var metadata = await dbContext.MetadataForms.FindAsync(id);
            if (metadata != null)
            {
                dbContext.MetadataForms.Remove(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Metadata failed to be deleted." });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, string status, string comment)
        {
            var metadata = await dbContext.MetadataForms.FindAsync(id);
            if (metadata != null)
            {
                metadata.Status = status;
                metadata.DocumentComment = comment; // Update DocumentComment with the comment
                dbContext.MetadataForms.Update(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = $"Document {status.ToLower()} successfully." });
            }
            return Json(new { success = false, error = "Update failed." });
        }

        /*   // POST: DownloadDocument
           [HttpPost]
           public IActionResult DownloadDocument(Guid id)
           {
             var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.Id == id);
             if (metadata != null)
             {
               return File(metadata.DocumentContent, "application/octet-stream", metadata.DocumentName);
             }
             return NotFound();
           }
       */
        // New action: GetDocumentSize
        [HttpPost]
        public IActionResult GetDocumentSize(Guid id)
        {
            try
            {
                var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.DocumentId == id);
                if (metadata != null)
                {
                    double sizeInBytes = metadata.DocumentContent.Length;
                    string sizeFormatted = FormatFileSize(sizeInBytes);
                    return Json(new { success = true, size = sizeFormatted });
                }
                return Json(new { success = false, error = "Metadata not found." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "An error occurred: " + ex.Message });
            }
        }

        // Helper method to format file size
        private string FormatFileSize(double bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide((decimal)bytes, max), order);

                max /= scale;
            }
            return "0 Bytes";
        }
        [HttpGet]
        public async Task<IActionResult> EditForm(Guid id)
        {
            var metadata = await dbContext.MetadataForms.FindAsync(id);
            if (metadata == null)
            {
                return NotFound();
            }

            var viewModel = new AddMetadata
            {
                DocumentId = metadata.DocumentId,
                DocumentName = metadata.DocumentName,
                DocumentType = metadata.DocumentType,
                DocumentFormat = metadata.DocumentFormat,
                DocumentDescription = metadata.DocumentDescription,
                OriginalExtension = metadata.OriginalExtension,

                // ExistingFilePath = metadata.ExistingFilePath // Assuming you have this field
            };

            /*ViewBag.Options = new SelectList(new List<string> { "Option1", "Option2", "Option3" }); // Add your options here*/
            ViewBag.Options = new SelectList(dbContext.DocumentType, "DocumentTypeOptionValue", "DocumentTypeOptionValue");

            return View("EditForm", viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> EditForm(AddMetadata viewModel)
        {
            if (ModelState.IsValid)
            {
                var metadata = await dbContext.MetadataForms.FindAsync(viewModel.DocumentId);
                if (metadata == null)
                {
                    return NotFound();
                }

                metadata.DocumentName = viewModel.DocumentName;
                metadata.DocumentType = viewModel.DocumentType;
                metadata.DocumentFormat = viewModel.DocumentFormat;
                metadata.DocumentDescription = viewModel.DocumentDescription;
                metadata.OriginalExtension = viewModel.OriginalExtension;
                ViewBag.Options = new SelectList(dbContext.DocumentType, "DocumentTypeOptionValue", "DocumentTypeOptionValue");
                metadata.Status = "Pending";

                if (viewModel.DocumentFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.DocumentFile.CopyToAsync(memoryStream);
                        metadata.DocumentContent = memoryStream.ToArray();
                        metadata.DocumentName = viewModel.DocumentFile.FileName;
                    }
                }

                dbContext.MetadataForms.Update(metadata);
                await dbContext.SaveChangesAsync();
                /*return RedirectToAction("DocumentManagement");*/
                return Json(new { success = true });
            }

            else
            {
                return Json(new { success = false, error = "Metadata update failed." });
            }

        }

        // ShortcutCreator action method
        public ActionResult ShortcutCreator()
        {
            // Hardcoded URL
            string url = "https://localhost:7157/Home/DocumentManagement";


            // Create the URL file content
            string urlFileContent = $"[{Guid.NewGuid():B}]";
            urlFileContent += Environment.NewLine;
            urlFileContent += "[InternetShortcut]";
            urlFileContent += Environment.NewLine;
            urlFileContent += $"URL={url}";


            // Return the .url file as a response
            var bytes = Encoding.UTF8.GetBytes(urlFileContent);
            return File(bytes, "application/octet-stream", "DocumentListShortcut.url");
        }

        private readonly ProtocolHandlerRegistrationService protocolHandlerRegistrationService;

        public class CreateShortcutController
        {
            private readonly ProtocolHandlerRegistrationService protocolHandlerRegistrationService;

            public CreateShortcutController(ProtocolHandlerRegistrationService protocolHandlerRegistrationService)
            {
                this.protocolHandlerRegistrationService = protocolHandlerRegistrationService;
            }
        }


        public ActionResult GetDocument(Guid documentId)
        {
            var document = dbContext.MetadataForms.Find(documentId);
                if (document != null)
            {
                var fileBytes = document.DocumentContent; // Assuming Content is a byte array
                var fileName = document.DocumentName;
                var fileExtension = document.OriginalExtension;

                // Return a FileContentResult with the file bytes
                return File(fileBytes, $"application/{fileExtension}", fileName);
            }
            else
            {
                return RedirectToAction("DocumentManagement");
            }
        }


        private IActionResult GetDocumentFromDatabase(Guid id)
        {
            // Retrieve the document from the database
            byte[] documentContent = dbContext.MetadataForms.Where(d => d.DocumentId == id).Select(d => d.DocumentContent).FirstOrDefault();

            if (documentContent == null)
            {
                return NotFound();
            }

            return File(documentContent, "application/octet-stream", "document." + GetDocumentExtension(id));
        }

        /*public IActionResult DownloadDocumentDb(string filePath) 
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath); // Delete the temporary file

            return File(fileBytes, "application/msword", "document.docx");
        }*/

        
        public IActionResult CreateShortcutAction()
        {
            if (protocolHandlerRegistrationService != null)
            {
                protocolHandlerRegistrationService.RegisterProtocolHandler();
            }
            else
            {
                return Content("Protocol handler unsuccessful.");
            }
            return RedirectToAction("CreateShortcutAction");
        }


        [HttpGet]
        public ActionResult CreateShortcut()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateShortcut(Guid documentId)
        {

            var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.DocumentId == documentId);
            if (metadata != null && metadata.DocumentContent != null && !string.IsNullOrEmpty(metadata.DocumentName))
            {
                // Retrieve document bytes from database
                byte[] documentBytes = metadata.DocumentContent;

                ApiClient apiClient = new ApiClient();
                await apiClient.CreateShortcut(documentBytes);


            }
            return View();
        }


        /*[HttpPost]
        public async Task<ActionResult> CreateShortcut(Guid documentId)
        {

            var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.DocumentId == documentId);
            if (metadata != null && metadata.DocumentContent != null && !string.IsNullOrEmpty(metadata.DocumentName))
            { 
                // Retrieve document bytes from database
                byte[] documentBytes = metadata.DocumentContent;

            ApiClient apiClient = new ApiClient();
            await apiClient.CreateShortcut(documentBytes);

            
            }
            return RedirectToAction("CreateShortcut");
        }*/


        /*public ActionResult CreateShortcut(int documentId)
        {
            // Create a new file with a.lnk extension
            var shortcutFile = new FileInfo($"Document_{documentId}.lnk");

            // Create a shell link object
            var shellLink = new ShellLink();

            // Set the target path to a custom protocol handler (e.g. "myapp://")
            shellLink.TargetPath = $"myapp://{documentId}";

            // Set the working directory to the application directory
            shellLink.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Set the icon location to the application icon
            shellLink.IconLocation = @"C:\Path\To\Application.ico";

            // Save the shortcut file
            shellLink.Save(shortcutFile.FullName);

            // Return the shortcut file
            return File(shortcutFile.FullName, "application/octet-stream", shortcutFile.Name);
        }*/


        /*private byte[] GetDocumentContent(int documentId)
        {
            return new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
        }

        public ActionResult OpenDocument(int documentId)
        {
            try
            {
                // Retrieve document content form the database
                byte[] documentContent = GetDocumentContent(documentId);

                // Save content to a temporary file (you can use a unique filename)
                string tempFilePath = Path.Combine(Path.GetTempPath(), $"Ali..pdf");
                System.IO.File.WriteAllBytes(tempFilePath, documentContent);

                // Return the file for download
                *//*return File(tempFilePath, "application/vnd.openxmlformats-officedocument.wprdprocessing.dpcument", "MyDocument.docx");*//*
                return File(tempFilePath, "application/pdf", "MyDocument.pdf");
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., document not found)
                return Content($"Error: {ex.Message}");
            }
        }*/

        /*public async Task<IActionResult> GetTotalPending(Guid id)
        {
            var pending = await dbContext.MetadataForms.FindAsync(id);

            int totalDocuments = pending != null ? pending.Count() : 0;
            int totalFinished = 0;
            foreach (var item in pending)
            {
                if (item.Status == "Approved" || item.Status == "Rejected")
                {
                    totalFinished++;
                }
            }
            int totalPending = totalDocuments - totalFinished;
            return Json(totalPending, JsonRequestBehavior.AllowGet);
        }*/



        /*    [HttpGet]
            public async Task<IActionResult> EditForm(Guid id)
            {
                var metadata = await dbContext.MetadataForms.FindAsync(id);
                if (metadata == null)
                {
                    return NotFound();
                }

                var viewModel = new AddMetadata
                {
                    Id = metadata.Id,
                    DocumentName = metadata.DocumentName,
                    DocumentType = metadata.DocumentType,
                    DocumentFormat = metadata.DocumentFormat,
                    DocumentTest = metadata.DocumentTest,
                    OriginalExtension = metadata.OriginalExtension //

                    *//*ExistingFilePath = metadata.ExistingFilePath // Assuming you have this field*//*
                };
                return View("Metadata", viewModel);
            }

            [HttpPost]
            public async Task<IActionResult> EditForm(AddMetadata viewModel)
            {
                if (ModelState.IsValid)
                {
                    var metadata = await dbContext.MetadataForms.FindAsync(viewModel.Id);
                    if (metadata == null)
                    {
                        return NotFound();
                    }

                    metadata.DocumentName = viewModel.DocumentName;
                    metadata.DocumentType = viewModel.DocumentType;
                    metadata.DocumentFormat = viewModel.DocumentFormat;
                    metadata.DocumentTest = viewModel.DocumentTest;


                    if (viewModel.DocumentFile != null)
                    {
                        // Save new file logic here, e.g., save to wwwroot/uploads and update metadata.DocumentFilePath
                   *//*     if (viewModel.DocumentFile != null)
                        {
                            var fileName = Path.GetFileName(viewModel.DocumentFile.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await viewModel.DocumentFile.CopyToAsync(stream);
                            }
                            metadata.DocumentFilePath = filePath;
                        }*//*
                    }

                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("DocumentManagement");
                }

                return View("Metadata", viewModel);
            }

    */

        /*
         * second
         * using Microsoft.AspNetCore.Hosting;
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.Extensions.Logging;
        using System;
        using System.IO;
        using System.Linq;
        using Microsoft.EntityFrameworkCore;
        using System.Threading.Tasks;
        using WebApplication2.Data;
        using WebApplication2.Models;
        using WebApplication2.Models.Entities;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Identity;
        using System.Security.Claims;

        namespace WebApplication2.Controllers
        {
          [Authorize]
          public class HomeController : Controller
          {
            private readonly ApplicationDbContext dbContext;
            private readonly IWebHostEnvironment _webHost;

            public HomeController(IWebHostEnvironment webHost, ApplicationDbContext dbContext)
            {
              _webHost = webHost;
              this.dbContext = dbContext;
            }

            public IActionResult Index()
            {
              return View();
            }

            public IActionResult Privacy()
            {
              return View();
            }

            public IActionResult Register()
            {
              return View();
            }

            public IActionResult Login()
            {
              return View();
            }

            public IActionResult Approver()
            {
              var items = dbContext.MetadataForms.ToList();
              return View(items);
            }

            public IActionResult DocumentManagement()
            {
              var items = dbContext.MetadataForms.ToList(); // Fetch metadata entries from the database
              return View(items); // Pass the metadata entries to the view
            }

            // Add this action to display the metadata form
            public IActionResult Metadata()
            {
              return View(new AddMetadata());
            }

            [HttpPost]
            public async Task<IActionResult> Metadata(AddMetadata viewModel)
            {
              if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
              {
                using (var memoryStream = new MemoryStream())
                {
                  await viewModel.DocumentFile.CopyToAsync(memoryStream);
                  // Extract user information from claims
                  var userClaims = User.Identity as ClaimsIdentity;
                  var userName = userClaims?.FindFirst("name")?.Value ??"unknwn";
                  var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ??"Unknown ";

                  var metadata = new Metadata
                  {
                    DocumentName = viewModel.DocumentName,
                    DocumentType = viewModel.DocumentType,
                    DocumentFormat = viewModel.DocumentFormat,
                    DocumentTest = viewModel.DocumentTest,
                    DocumentContent = memoryStream.ToArray(),
                    Status = "Pending", // default status
                    UploadDate = ConvertToMalaysianTime(DateTime.UtcNow),
                    CreatedBy = userName,
                    CreatedByEmail =userEmail


                  };

                  await dbContext.MetadataForms.AddAsync(metadata);
                  await dbContext.SaveChangesAsync();
                  return Json(new { success = true }); // Added this to return success response
                }
              }
              else
              {
                return Json(new { success = false, error = "File upload failed." });
              }
            }
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
              }
            }
            // Add this action to handle deletion of metadata entries
            [HttpPost]
            public async Task<IActionResult> Delete(Guid id)
            {
              var metadata = await dbContext.MetadataForms.FindAsync(id);
              if (metadata != null)
              {
                dbContext.MetadataForms.Remove(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Metadata deleted successfully." });
              }
              return Json(new { success = false, error = "Deletion failed." });
            }

            [HttpPost]
            public async Task<IActionResult> UpdateStatus(Guid id, string status)
            {
              var metadata = await dbContext.MetadataForms.FindAsync(id);
              if (metadata != null)
              {
                metadata.Status = status;
                dbContext.MetadataForms.Update(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = $"Document {status.ToLower()} successfully." });
              }
              return Json(new { success = false, error = "Update failed." });
            }

            // POST: DownloadDocument
            [HttpPost]
            public IActionResult DownloadDocument(Guid id)
            {
              var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.Id == id);
              if (metadata != null)
              {
                return File(metadata.DocumentContent, "application/octet-stream", metadata.DocumentName);
              }
              return NotFound();
            }

            // New action: GetDocumentSize
            [HttpPost]
            public IActionResult GetDocumentSize(Guid id)
            {
              try
              {
                var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.Id == id);
                if (metadata != null)
                {
                  double sizeInBytes = metadata.DocumentContent.Length;
                  string sizeFormatted = FormatFileSize(sizeInBytes);
                  return Json(new { success = true, size = sizeFormatted });
                }
                return Json(new { success = false, error = "Metadata not found." });
              }
              catch (Exception ex)
              {
                return Json(new { success = false, error = "An error occurred: " + ex.Message });
              }
            }

            // Helper method to format file size
            private string FormatFileSize(double bytes)
            {
              const int scale = 2024;
              string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
              long max = (long)Math.Pow(scale, orders.Length - 1);

              foreach (string order in orders)
              {
                if (bytes > max)
                  return string.Format("{0:##.##} {1}", decimal.Divide((decimal)bytes, max), order);

                max /= scale;
              }
              return "0 Bytes";
            }

        */




        /*
         * first
         * using Microsoft.AspNetCore.Hosting;
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.Extensions.Logging;
        using System;
        using System.IO;
        using System.Linq;
        using System.Threading.Tasks;
        using WebApplication2.Data;
        using WebApplication2.Models;
        using WebApplication2.Models.Entities;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.Graph;
        using Microsoft.Identity.Client;


        namespace WebApplication2.Controllers
        {
          [Authorize]
          public class HomeController : Controller
          {
            private readonly ApplicationDbContext dbContext;
            private readonly IWebHostEnvironment _webHost;
            private readonly GraphServiceClient _graphServiceClient;

            public HomeController(IWebHostEnvironment webHost, ApplicationDbContext dbContext, GraphServiceClient graphServiceClient)
            {
              _webHost = webHost;
              this.dbContext = dbContext;
              _graphServiceClient = graphServiceClient;
            }

            public IActionResult Index()
            {
              return View();
            }

            public IActionResult Privacy()
            {
              return View();
            }

            public IActionResult Register()
            {
              return View();
            }

            public IActionResult Login()
            {
              return View();
            }

            public IActionResult Approver()
            {
              var items = dbContext.MetadataForms.ToList();
              return View(items);
            }

            public IActionResult DocumentManagement()
            {
              var items = dbContext.MetadataForms.ToList();
              return View(items);
            }

            public IActionResult Metadata()
            {
              return View(new AddMetadata());
            }

            [HttpPost]
            public async Task<IActionResult> Metadata(AddMetadata viewModel)
            {
              if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
              {
                using (var memoryStream = new MemoryStream())
                {
                  await viewModel.DocumentFile.CopyToAsync(memoryStream);

                  // Retrieve user information from Azure AD
                  var user = await _graphServiceClient.Me.GetAsync();

                  var metadata = new Metadata
                  {
                    DocumentName = viewModel.DocumentName,
                    DocumentType = viewModel.DocumentType,
                    DocumentFormat = viewModel.DocumentFormat,
                    DocumentTest = viewModel.DocumentTest,
                    DocumentContent = memoryStream.ToArray(),
                    Status = "Pending",
                    CreatedBy = user.DisplayName,
                    CreatedByEmail = user.Mail
                  };

                  dbContext.MetadataForms.Add(metadata);
                  await dbContext.SaveChangesAsync();
                  return Json(new { success = true });
                }
              }
              else
              {
                return Json(new { success = false, error = "File upload failed." });
              }
            }

            [HttpPost]
            public async Task<IActionResult> Delete(Guid id)
            {
              var metadata = await dbContext.MetadataForms.FindAsync(id);
              if (metadata != null)
              {
                dbContext.MetadataForms.Remove(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Metadata deleted successfully." });
              }
              return Json(new { success = false, error = "Deletion failed." });
            }

            [HttpPost]
            public async Task<IActionResult> UpdateStatus(Guid id, string status)
            {
              var metadata = await dbContext.MetadataForms.FindAsync(id);
              if (metadata != null)
              {
                metadata.Status = status;
                dbContext.MetadataForms.Update(metadata);
                await dbContext.SaveChangesAsync();
                return Json(new { success = true, message = $"Document {status.ToLower()} successfully." });
              }
              return Json(new { success = false, error = "Update failed." });
            }

            [HttpPost]
            public IActionResult DownloadDocument(Guid id)
            {
              var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.Id == id);
              if (metadata != null)
              {
                return File(metadata.DocumentContent, "application/octet-stream", metadata.DocumentName);
              }
              return NotFound();
            }

            [HttpPost]
            public IActionResult GetDocumentSize(Guid id)
            {
              try
              {
                var metadata = dbContext.MetadataForms.FirstOrDefault(m => m.Id == id);
                if (metadata != null)
                {
                  double sizeInBytes = metadata.DocumentContent.Length;
                  string sizeFormatted = FormatFileSize(sizeInBytes);
                  return Json(new { success = true, size = sizeFormatted });
                }
                return Json(new { success = false, error = "Metadata not found." });
              }
              catch (Exception ex)
              {
                return Json(new { success = false, error = "An error occurred: " + ex.Message });
              }
            }

            private string FormatFileSize(double bytes)
            {
              const int scale = 1024;
              string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
              long max = (long)Math.Pow(scale, orders.Length - 1);

              foreach (string order in orders)
              {
                if (bytes > max)
                  return string.Format("{0:##.##} {1}", decimal.Divide((decimal)bytes, max), order);

                max /= scale;
              }
              return "0 Bytes";
            }

        */

        [HttpPost]
    public async Task<IActionResult> MetadataForm(IFormFile file)
    {
      string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      string fileName = Path.GetFileName(file.FileName);
      string fileSavePath = Path.Combine(uploadsFolder, fileName);

      using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      ViewBag.Message = fileName + " uploaded successfully";

      return View();
    }

    /*[HttpPost]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected");
            }

            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "DANIEL");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, file.FileName);
     
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Content(file.FileName + " created successfully");
        }*/

        /*[HttpGet]
        public IActionResult GetDocument(Guid id)
        {
            // Retrieve the document from the database
            byte[] documentContent = dbContext.MetadataForms.Where(d => d.DocumentId == id).Select(d => d.DocumentContent).FirstOrDefault();

            if (documentContent == null)
            {
                return NotFound();
            }

            return File(documentContent, "application/octet-stream", "document." + GetDocumentExtension(id));
        }*/

        private string GetDocumentExtension(Guid id)
        {
            // Retrieve the document extension from the database
            string extension = dbContext.MetadataForms.Where(d => d.DocumentId == id).Select(d => d.OriginalExtension).FirstOrDefault();
            return extension;
        }

        public IActionResult UploadDocument()
        {
            return View(new AddDocument());
        }

        /* [HttpPost]
         public async Task<IActionResult> UploadDocument(AddDocument viewModel)
         {

             if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
             {
                 using (var memoryStream = new MemoryStream())
                 {
                     await viewModel.DocumentFile.CopyToAsync(memoryStream);

                     // Extract user information from claims
                     var userClaims = User.Identity as ClaimsIdentity;
                     var userName = userClaims?.FindFirst("name")?.Value ?? "unknown";
                     var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";

                     var uploadDocument = new UploadDocument
                     {
                         DocumentName = viewModel.DocumentName,
                         DocumentContent = memoryStream.ToArray(),
                         UploadDate = ConvertToMalaysianTime(DateTime.UtcNow),
                         CreatedBy = userName,
                         CreatedByEmail = userEmail
                     };

                     await dbContext.UploadDocuments.AddAsync(uploadDocument);
                     await dbContext.SaveChangesAsync();
                     return Json(new { success = true }); // To return success response
                 }
             }
             else
             {
                 return Json(new { success = false, error = "Document upload failed." });
             }
         }*/

        /*[HttpPost]
        public async Task<IActionResult> UploadDocument(IEnumerable<IFormFile> DocumentFile, string DocumentName)
        {
            if (DocumentFile != null && DocumentFile.Any())
            {
                foreach (var file in DocumentFile)
                {
                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            // Extract user information from claims
                            var userClaims = User.Identity as ClaimsIdentity;
                            var userName = userClaims?.FindFirst("name")?.Value ?? "unknown";
                            var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";

                            var uploadDocument = new UploadDocument
                            {
                                DocumentName = DocumentName,
                                DocumentContent = memoryStream.ToArray(),
                                UploadDate = ConvertToMalaysianTime(DateTime.UtcNow),
                                CreatedBy = userName,
                                CreatedByEmail = userEmail
                            };

                            await dbContext.UploadDocuments.AddAsync(uploadDocument);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
                return Json(new { success = true }); // To return success response
            }
            else
            {
                return Json(new { success = false, error = "Document upload failed." });
            }
        }*/

        [HttpPost]
        public async Task<IActionResult> UploadDocument(AddDocument viewModel)
        {
            if (viewModel.DocumentFile != null && viewModel.DocumentFile.Length > 0)
            {
                foreach (var file in viewModel.DocumentFile)
                {
                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            // Extract user information from claims
                            var userClaims = User.Identity as ClaimsIdentity;
                            var userName = userClaims?.FindFirst("name")?.Value ?? "unknown";
                            var userEmail = userClaims?.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";

                            var uploadDocument = new UploadDocument
                            {
                                DocumentName = viewModel.DocumentName,
                                DocumentContent = memoryStream.ToArray(),
                                UploadDate = ConvertToMalaysianTime(DateTime.UtcNow),
                                CreatedBy = userName,
                                CreatedByEmail = userEmail,
                                OriginalExtension = viewModel.OriginalExtension
                            };

                            await dbContext.UploadDocuments.AddAsync(uploadDocument);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
                return Json(new { success = true }); // To return success response
            }
            else
            {
                return Json(new { success = false, error = "Document upload failed." });
            }
        }

        [HttpGet]
        public IActionResult DownloadUploadedDocument(Guid id)
        {
            var uploadDocument = dbContext.UploadDocuments.FirstOrDefault(m => m.DocumentId == id);
            if (uploadDocument != null && uploadDocument.DocumentContent != null && !string.IsNullOrEmpty(uploadDocument.DocumentName))
            {
                // Mapping of file extensions to MIME types  
                var mimeTypes = new Dictionary<string, string>
        {
            {".pdf", "application/pdf"},
            {".doc", "application/msword"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            // Add more mappings as needed  
        };

                // Use the stored original extension
                string extension = uploadDocument.OriginalExtension;

                // Get the MIME type  
                string mimeType = mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octet-stream";

                var contentDisposition = new ContentDisposition
                {
                    FileName = $"{Path.GetFileNameWithoutExtension(uploadDocument.DocumentName)}{extension}",
                    Inline = false  // false = prompt the user for downloading; true = browser to try to show the file inline  
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(uploadDocument.DocumentContent, mimeType);
            }
            return NotFound();
        }



        public IActionResult DocumentTable()
        {
            var items = dbContext.UploadDocuments.ToList(); // Fetch document entries from the database
            return View(items); // Pass the document entries to the view
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (dbContext != null))
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
















