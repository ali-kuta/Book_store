﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace firs_dot_net_project.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }


        [Required]
        [DisplayName("List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }


        [Required]
        [DisplayName("1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }


        [Required]
        [DisplayName("price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }


        [Required]
        [DisplayName("Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category categories { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }
    }
}
