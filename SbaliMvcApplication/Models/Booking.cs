using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SbaliMvcApplication.Models
{

    public class Booking
    {
        [Required()]
        public string TimeSlot { get; set; }
        [Required()]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required()]
        public string Cell { get; set; }
        [Required()]
        public string Location { get; set; }
        [Required()]
        public string Surbub { get; set; }
        [Required()]
        public string Problem { get; set; }

        public Booking() {
            Problem = string.Empty;
            TimeSlot = string.Empty;
        }
    }

    public class BookingRefenceNo
    {
        public long Number { get; set; }
        public string Name { get; set; }
    }



}