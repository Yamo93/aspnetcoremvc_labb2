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
    public class UpdateTaskController : Controller
    {
        public IActionResult Tasks(TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.Id = taskModel.Id;
                taskModel.UpdatedAt = DateTime.Now;
                var jsonStr = System.IO.File.ReadAllText("tasks.json");
                var jsonObj = JsonConvert.DeserializeObject<List<TaskModel>>(jsonStr);
                // Uppdaterar uppgiften vid indexet
                jsonObj[jsonObj.FindIndex(i => i.Id == taskModel.Id)] = taskModel;
                ViewBag.Tasks = jsonObj;
                System.IO.File.WriteAllText("tasks.json", JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

                ModelState.Clear();
            }

            return RedirectToAction("Tasks", "Home");
        }
    }
}