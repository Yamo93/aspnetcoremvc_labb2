﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_MVC_Labb2.Models
{
    public class TaskModel
    {
        public string Id { get; set; }
        [Display(Name="Namn")]
        [Required(ErrorMessage = "Fyll i namn.")]
        public string Name { get; set; }
        [Display(Name = "Kursnamn")]
        [Required(ErrorMessage = "Fyll i kursnamn.")]
        public string CourseName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TaskModel()
        {
            IsCompleted = false;
        }
    }
}
