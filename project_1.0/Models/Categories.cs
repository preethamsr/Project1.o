using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_1._0.Models
{
    [Table("Category")]
    public class Categories
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}