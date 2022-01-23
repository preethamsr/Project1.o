using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using project_1._0.Models;

namespace project_1._0.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Adduser([FromBody] users userdata)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    bool email = emailverification(userdata.Email);
                    if (!email)
                    {

                        userdata.Password = textToEncrypt(userdata.Password);
                        userdata.Dataofcreation = DateTime.Now;
                        projectcontext.user.Add(userdata);
                        projectcontext.SaveChanges();
                        var saveddata = projectcontext.user.Where(x => x.Email == userdata.Email).Select(z => z.ID).FirstOrDefault();
                        SendEmailToUser(userdata.Email, saveddata);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, "Email already Exits");
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public bool emailverification(string email)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    var check = projectcontext.user.Where(x => x.Email == email).ToList();
                    return check != null;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public void SendEmailToUser(string emailId, int id)
        {
            var GenarateUserVerificationLink = "/User/Userverification?id=" + id;
            var link = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, GenarateUserVerificationLink);
            var fromMail = new MailAddress("laleshraj716@gmail.com", "ranjitha716"); // set your email  
            var fromEmailpassword = "ranjitha716"; // Set your password
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed-Demo";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }
        public static string textToEncrypt(string paasWord)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(paasWord)));
        }

        [HttpPost]
        public HttpResponseMessage Userverification(int id)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    var userdata = projectcontext.user.Where(x => x.ID == id).FirstOrDefault();
                    if (userdata != null)
                    {
                        userdata.Verified = true;
                        projectcontext.Entry(userdata).State = System.Data.Entity.EntityState.Modified;
                        projectcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPost]
        public HttpResponseMessage Userlogin([FromBody] users usersdata)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    string password = textToEncrypt(usersdata.Password);
                    var userverification = projectcontext.user.Where(x => x.Email == usersdata.Email && x.Password == password && x.Verified == true).FirstOrDefault();
                    if (userverification != null)
                    {
                        userverification.Lastlogin = DateTime.Now;
                        userverification.Langitude = usersdata.Langitude;
                        userverification.Latitude = usersdata.Latitude;
                        userverification.Ipaddress = usersdata.Ipaddress;
                        projectcontext.Entry(userverification).State = System.Data.Entity.EntityState.Modified;
                        projectcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, userverification.ID);
                    }


                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage Forgotpassword([FromBody] users userdata)
        {
            using (project projectcontext = new project())
            {
                try
                {
                    var forgotpassword = projectcontext.user.Where(x => x.Email == userdata.Email).FirstOrDefault();
                    if (forgotpassword != null)
                    {
                        string password = textToEncrypt(userdata.Password);
                        forgotpassword.Password = password;
                        projectcontext.Entry(forgotpassword).State = System.Data.Entity.EntityState.Modified;
                        projectcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
