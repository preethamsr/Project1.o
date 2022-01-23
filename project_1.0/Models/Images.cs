using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_1._0.Models
{
    [Table("Images")]
    public class Images
    {
        [Key]
        public int ID { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        [ForeignKey("User")]
        public int User { get; set; }
        [ForeignKey("Post")]
        public int Post { get; set; }

        public Nullable<System.DateTime> Createdon { get; set; }


    }
}