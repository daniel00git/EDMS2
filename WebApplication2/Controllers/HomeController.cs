/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
  public class HomeController : Controller
  {
    private readonly IWebHostEnvironment _webHost;
    public HomeController(IWebHostEnvironment webHost)
    {
      _webHost = webHost;
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
    public IActionResult DocumentManagement()
    {
      return View();
    }

    *//*  Upload Document*//*
    public IActionResult MetadataForm()
    {
      return View();
    }

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
    *//*CreatedNewDocument*//*

    [HttpPost]
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

      return Content(file.FileName + " Created successfully");
    }
 
  }
}*/








/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WebApplication2.Data; // Ensure this namespace is correct
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
  public class HomeController : Controller
  {
    private readonly IWebHostEnvironment _webHost;
    private readonly ApplicationDbContext _context;

    public HomeController(IWebHostEnvironment webHost, ApplicationDbContext context)
    {
      _webHost = webHost;
      _context = context;
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

    public IActionResult DocumentManagement()
    {
      return View();
    }

    *//* Upload Document *//*
    public IActionResult MetadataForm()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> MetadataForm(MetadataForm model, IFormFile file)
    {
      if (ModelState.IsValid)
      {
        string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
          Directory.CreateDirectory(uploadsFolder);
        }

        if (file != null && file.Length > 0)
        {
          string fileName = Path.GetFileName(file.FileName);
          string filePath = Path.Combine(uploadsFolder, fileName);

          using (FileStream stream = new FileStream(filePath, FileMode.Create))
          {
            await file.CopyToAsync(stream);
          }

          model.FilePath = filePath;
        }

        _context.Add(model);
        await _context.SaveChangesAsync();

        ViewBag.Message = "Document uploaded successfully";
        return View();
      }

      ViewBag.Message = "There was an error uploading the document";
      return View(model);
    }

    *//* CreatedNewDocument *//*
    [HttpPost]
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
    }
  }
}*/



using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;  // Add this
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
  public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;  // Add this

    public HomeController(IWebHostEnvironment webHost, ApplicationDbContext context, ILogger<HomeController> logger)  // Modify this
    {
            _webHost = webHost;
            _context = context;
           _logger = logger;
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

        public IActionResult DocumentManagement()
        {
            return View();
        }

        public IActionResult MetadataForm()
        {
            return View();
        }
    
            [HttpPost]
            public async Task<IActionResult> MetadataForm(MetadataForm model, IFormFile file)
            {
                if (ModelState.IsValid)
                {
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    if (file != null && file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        model.FilePath = filePath;
                    }

                    _context.Add(model);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "Document uploaded successfully";
                    return View();
                }


                ViewBag.Message = "There was an error uploading the document";
                return View(model);
            }
   

    [HttpPost]
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
        }
    }
}


















/*  private readonly ILogger<HomeController> _logger;*/
/*public HomeController(ILogger<HomeController> logger)
{
  _logger = logger;
}
*/


/*{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\ProjectModels;Initial Catalog=DocumentManagementDb;Integrated Security=True;Trust Server Certificate=True"
  }
}*/
