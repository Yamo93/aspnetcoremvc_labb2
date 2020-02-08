using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ASP_NET_MVC_Labb2.Models
{
    public class UsernameModel
    {
        [Display(Name = "Namn")]
        public string Name { get; set; }
    }
}
