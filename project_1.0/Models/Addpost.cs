using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_1._0.Models
{
    [Table("Post")]
    public class Addpost
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int User { get; set; }

        public DateTime Createdon { get; set; }
        public Nullable<System.DateTime> Modifiedon { get; set; }
    }
}