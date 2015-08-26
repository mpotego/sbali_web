using CaptchaMvc.HtmlHelpers;
using SbaliMvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;



namespace SbaliMvcApplication.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/

        public ActionResult Index()
        {
            ViewBag.activeTab = "Book";

            List<SelectListItem> problemOption = GetProblems();
            List<SelectListItem> timeSlotsOptions = GetTimeSlots();
            ViewBag.problems = problemOption;
            ViewBag.timeSlots = timeSlotsOptions;

            return View();


        }

        [HttpPost]
        public ActionResult Index(Booking booking)
        {
            ViewBag.activeTab = "Book";

            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                List<SelectListItem> problemOption = GetProblems();
                List<SelectListItem> timeSlotsOptions = GetTimeSlots();
                ViewBag.problems = problemOption;
                ViewBag.timeSlots = timeSlotsOptions;

                ViewBag.CaptchaErrMessage = "Please enter correct value";
            }
            else
            {
                string timeslot = GetSelectedTimeSlots(booking.TimeSlot);
                string problem = GetSelectedProblems(booking.Problem);
                string bookingBody = BuildEmailBody(booking);
                string bookingsubject = string.Format("{0} {1}", timeslot, problem);
                string referenceNumber = GenerateRefNo();
                bool emailSent = false;
                emailSent = SendEmail(bookingsubject, bookingBody, "RefNo:" + referenceNumber + " ");
                if (emailSent == true)
                {
                    return RedirectToAction("BookingSuccess", "Booking", new { bookingRefNo = referenceNumber });
                }
                else
                {
                    List<SelectListItem> problemOption = GetProblems();
                    List<SelectListItem> timeSlotsOptions = GetTimeSlots();
                    ViewBag.problems = problemOption;
                    ViewBag.timeSlots = timeSlotsOptions;

                    ViewBag.Error = "Booking Failed - A technical error happned. Please try agian later";
                }
            }

            return View();
        }

        public ActionResult BookingSuccess(string bookingRefNo)
        {
            ViewBag.activeTab = "Book";
            ViewBag.BookingRefenceNo = bookingRefNo;

            return View();
        }

        public string BuildEmailBody(Booking booking)
        {
            string timeSlot = GetSelectedTimeSlots(booking.TimeSlot);
            string problem = GetSelectedProblems(booking.Problem);

            string body =
                 string.Format("Time : {0} <br /> Problem : {1} <br /> First Name{2} <br />"
                 + "Last Name : {3} <br /> Cell Number : {4} <br /> Email Address : {5}"
                 + "Location : {6} <br /> Surbub : {7}",
                 timeSlot,
                 problem,
                 booking.Name,
                 booking.Surname,
                 booking.Cell,
                 booking.Email,
                 booking.Location,
                 booking.Surbub
                 );

            return body;
        }

        public bool SendEmail(string subject, string body, string referenceNumber)
        {
            bool successfully = false;

            string from = "mpotego@gmail.com";
            string to = "mpotego@gmail.com";

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(from);
                mail.Subject = referenceNumber + subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("mpotego@gmail.com", "fddfPERCYVala");
                smtp.EnableSsl = true;
                //smtp.Send(mail);
                successfully = true;
            }
            catch (Exception e)
            {
                successfully = false;
            }
            return successfully;
        }

        public List<SelectListItem> GetTimeSlots()
        {
            return new List<SelectListItem>(){
                                 new SelectListItem(){ Value= string.Empty,Selected=true, Text="--Please select time--"},                
                                 new SelectListItem(){ Value= "1",  Text="6-8am"},
                                 new SelectListItem(){ Value= "2",  Text="8-10am"},
                                 new SelectListItem(){ Value= "3",  Text="10-12am"},
                                 new SelectListItem(){ Value= "4",  Text="12-2pm"},
                                 new SelectListItem(){ Value= "5",  Text="2-4pm"},
                                 new SelectListItem(){ Value= "6",  Text="4-6pm"},
                                 new SelectListItem(){ Value= "7",  Text="6-8pm"},
                                 new SelectListItem(){ Value= "8",  Text="Other Time"}};
        }

        public List<SelectListItem> GetProblems()
        {

            return new List<SelectListItem>(){
                new SelectListItem(){ Value= string.Empty, Selected=true, Text="--Please select problem--"},
                new SelectListItem(){ Value=1.ToString(), Text = "General Plumbing"},
                new SelectListItem(){ Value=2.ToString(),  Text="Blocked Drain (Inside or Outside)"},
                new SelectListItem(){ Value=3.ToString(),  Text="CCTV Drain Camera"},
                new SelectListItem(){ Value=4.ToString(),  Text="Geyser Related Problems"},
                new SelectListItem(){ Value=4.ToString(),  Text="Leak Detection"},
                new SelectListItem(){ Value=5.ToString(),  Text="Leak Alarm"},
                new SelectListItem(){ Value=6.ToString(),  Text="Leaking Toilet, Basins, Baths, Urinary"},
                new SelectListItem(){ Value=7.ToString(),  Text="Quotation"},
                new SelectListItem(){ Value=8.ToString(),  Text="Septic Tank and French Drain Pump Outs"},
                new SelectListItem(){ Value=9.ToString(),  Text="Water Leak Alarm"},
                new SelectListItem(){ Value=10.ToString(), Text="Other"}};



        }

        public string GetSelectedTimeSlots(string Id)
        {
            var timeSlots = new List<SelectListItem>(){
                                 new SelectListItem(){ Value= string.Empty,Selected=true, Text="--Please select time--"},                
                                 new SelectListItem(){ Value= "1",  Text="6-8am"},
                                 new SelectListItem(){ Value= "2",  Text="8-10am"},
                                 new SelectListItem(){ Value= "3",  Text="10-12am"},
                                 new SelectListItem(){ Value= "4",  Text="12-2pm"},
                                 new SelectListItem(){ Value= "5",  Text="2-4pm"},
                                 new SelectListItem(){ Value= "6",  Text="4-6pm"},
                                 new SelectListItem(){ Value= "7",  Text="6-8pm"},
                                 new SelectListItem(){ Value= "8",  Text="Other Time"}
            };
            
            return timeSlots.FirstOrDefault(x => x.Value == Id.ToString()).Text.ToString();

        }

        public string GetSelectedProblems(string Id)
        {

            var problems = new List<SelectListItem>(){
                new SelectListItem(){ Value= string.Empty, Selected=true, Text="--Please select problem--"},
                new SelectListItem(){ Value=1.ToString(), Text = "General Plumbing"},
                new SelectListItem(){ Value=2.ToString(),  Text="Blocked Drain (Inside or Outside)"},
                new SelectListItem(){ Value=3.ToString(),  Text="CCTV Drain Camera"},
                new SelectListItem(){ Value=4.ToString(),  Text="Geyser Related Problems"},
                new SelectListItem(){ Value=4.ToString(),  Text="Leak Detection"},
                new SelectListItem(){ Value=5.ToString(),  Text="Leak Alarm"},
                new SelectListItem(){ Value=6.ToString(),  Text="Leaking Toilet, Basins, Baths, Urinary"},
                new SelectListItem(){ Value=7.ToString(),  Text="Quotation"},
                new SelectListItem(){ Value=8.ToString(),  Text="Septic Tank and French Drain Pump Outs"},
                new SelectListItem(){ Value=9.ToString(),  Text="Water Leak Alarm"},
                new SelectListItem(){ Value=10.ToString(), Text="Other"}};

            return problems.FirstOrDefault(x => x.Value == Id.ToString()).Text.ToString();

        }

        public string GenerateRefNo() {
            string RefenceNumber = string.Empty;

            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            DateTime todayDate = DateTime.Now;

            string month = alphabets[todayDate.Month-1].ToString();
            int day = todayDate.Day;
            string year = todayDate.ToString("yy");
            string hourS = todayDate.ToString("HH");
            string hour = alphabets[Convert.ToInt32(hourS) - 1].ToString();
            string minutes = todayDate.ToString("mm");
            string seconds = todayDate.ToString("ss");

            RefenceNumber = month + day + year + hour + minutes + seconds;
            
            return RefenceNumber;
        }

    }
}
