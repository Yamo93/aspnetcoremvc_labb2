using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_MVC_Labb2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ASP_NET_MVC_Labb2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewData["BriefInfo"] = "Den här applikationen är till för att hantera planeringen av skoluppgifter.";

            return View();
        }
        [HttpPost]
        public IActionResult Index(UsernameModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                HttpContext.Session.SetString("username", model.Name);
                ViewBag.Username = model.Name;
            }

            return View();
        }

        [HttpGet("/uppgifter")]
        public IActionResult Tasks()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            var jsonStr = System.IO.File.ReadAllText("tasks.json");
            var jsonObj = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);
            ViewBag.Tasks = jsonObj;
            return View();
        }


        [HttpPost("/uppgifter")]
        public IActionResult Tasks(TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.Id = GenerateId();
                taskModel.CreatedAt = DateTime.Now;
                var jsonStr = System.IO.File.ReadAllText("tasks.json");
                var jsonObj = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);
                jsonObj.Add(taskModel);
                ViewBag.Tasks = jsonObj;
                System.IO.File.WriteAllText("tasks.json", JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

                ModelState.Clear();
            }

            return View("Tasks");
        }

        [HttpGet("/om")]
        public IActionResult About()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }

        private string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}