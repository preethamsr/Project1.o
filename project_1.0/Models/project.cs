using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project_1._0.Models
{
    public class project:DbContext
    {
        public DbSet<users> user { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Addpost> Addpost { get; set; }
        public DbSet<Images> images { get; set; }
    }
 
}