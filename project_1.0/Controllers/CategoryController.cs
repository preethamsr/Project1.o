using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project_1._0.Models;

namespace project_1._0.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Addcategory([FromBody] Categories categories)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    projectcontext.categories.Add(categories);
                    projectcontext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPut]
        public HttpResponseMessage Modifycategory([FromBody] Categories categories)
        {
            using(project projectcontext=new project())
            {
                try
                {
                    var category = projectcontext.categories.Where(x => x.ID == categories.ID).FirstOrDefault();
                    if(category!=null)
                    {
                        category.Name = categories.Name;
                        projectcontext.Entry(category).State = System.Data.Entity.EntityState.Modified;
                        projectcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }catch(Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpDelete]
        public HttpResponseMessage Deletecategory([FromBody] Categories categories)
        {
            using(project projectcontext=new project())
            { 
                try
                {
                    var categoryid = projectcontext.categories.Where(x => x.ID == categories.ID).FirstOrDefault();
                    projectcontext.Entry(categoryid).State = System.Data.Entity.EntityState.Deleted;
                    projectcontext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.InnerException);                
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
            
        }
        [HttpGet]
        public HttpResponseMessage Categorylist()
        {
            using(project projectcontext=new project())
            {
                try { 
                var categorylist = projectcontext.categories.ToList();
                if(categorylist.Count>0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, categorylist);
               }else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                }catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
    

}
