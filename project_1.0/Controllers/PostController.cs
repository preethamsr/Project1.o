using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using project_1._0.Models;

namespace project_1._0.Controllers
{
    public class PostController : ApiController
    {
        [HttpPost]

        public HttpResponseMessage Addpost([FromBody] Addpost post)
        {
            using (project projectcontext = new project())
            {
                //try
                //{
                //    Postlist list = new Postlist();
           
                //    Postlist postl = (Postlist)JsonDeserialize(postlist, list.GetType());
                //    Addpost addpost = new Addpost();
                //    addpost.Createdon = DateTime.Now;
                //    addpost.Description = postl.Description;
                //    addpost.Title = postl.Title;
                //    addpost.User = postl.postonwer;
                //    projectcontext.Addpost.Add(addpost);
                //    projectcontext.SaveChanges();
                    
                //    foreach(var post in postl.Imagelist)
                //    {
                //        Images img = new Images();
                //        img.Createdon = DateTime.Now;
                //        img.Post = addpost.ID;
                //        img.Size = post.Size;
                //        img.Url = post.Url;
                //        img.User = postl.postonwer;
                //        projectcontext.images.Add(img);
                //        projectcontext.Entry(img).State = System.Data.Entity.EntityState.Added;

                //    }
                //    projectcontext.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                //}

            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        public static object JsonDeserialize(string json, Type objType)
        {

            object result = null;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(objType);
                object obj = ser.ReadObject(ms);
                ms.Close();
                result = obj;
            }
            return result;
        }
    }



    public class Postlist
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int postonwer { get; set; }

        public IList<Images> Imagelist { get; set; }
    }
    public class Imgages
    {

        public string Url { get; set; }
        public int Size { get; set; }
        public int User { get; set; }


    }
}
