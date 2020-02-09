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
            var taskModel = new TaskModel();
            return View(taskModel);
        }


        [HttpPost("/uppgifter")]
        public IActionResult Tasks(TaskModel taskModel)
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            var jsonStr = System.IO.File.ReadAllText("tasks.json");
            var jsonObj = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);

            if (ModelState.IsValid)
            {
                taskModel.Id = GenerateId();
                taskModel.CreatedAt = DateTime.Now;
                jsonObj.Add(taskModel);
                System.IO.File.WriteAllText("tasks.json", JsonConvert.SerializeObject(jsonObj, Formatting.Indented));
                ModelState.Clear();
            }
            ViewBag.Tasks = jsonObj;
            var task = new TaskModel();
            return View(task);
        }

        [HttpGet("/om")]
        public IActionResult About()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            var info = new InformationViewModel { InformationalText = "Den här applikationen hanterar skoluppgifter. Man kan skapa uppgifter och hålla koll på dem, samt bocka av dem när de är färdiga." };
            return View(info);
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