﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name  { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="1 dej 100 o llud")] 
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now; 
    }
}
