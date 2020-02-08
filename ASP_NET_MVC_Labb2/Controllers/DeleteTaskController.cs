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
    public class DeleteTaskController : Controller
    {
        public IActionResult Tasks(TaskModel taskModel)
        {
            if (taskModel.Id != null)
            {
                var jsonStr = System.IO.File.ReadAllText("tasks.json");
                var jsonObj = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);
                // Raderar uppgiften vid indexet
                jsonObj.RemoveAt(jsonObj.FindIndex(i => i.Id == taskModel.Id));
                ViewBag.Tasks = jsonObj;
                System.IO.File.WriteAllText("tasks.json", JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

                ModelState.Clear();
            }

            return RedirectToAction("Tasks", "Home");
        }
    }
}