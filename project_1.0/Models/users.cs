using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_1._0.Models
{
    [Table("Users")]
    public class users
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phonenumber { get; set; }
        public bool Verified { get; set; }
        public DateTime Dataofcreation { get; set; }
        public string Langitude { get; set; }
        public string Latitude { get; set; }
        public string Ipaddress { get; set; }
        public Nullable<System.DateTime>  Lastlogin{ get; set; }
    }
}