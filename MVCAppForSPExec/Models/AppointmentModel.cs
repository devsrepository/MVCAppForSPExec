using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MVCAppForSPExec.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //[Required(ErrorMessage = "Mobile number is required")]
        //[RegularExpression(@"^\+[0-9]{1,3}-[0-9]{3,14}$", ErrorMessage = "Please enter a valid phone number.")]
        public string MobileNo { get; set; }
        public string AppointmentDate { get; set; }
        public string Gender { get; set; }
        public string Message { get; set; }
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string CreateDate { get; set; }
        public Nullable<int> State { get; set; }
        public int? City { get; set; }
        public string CityName { get; set; }
        public string  StateName{ get; set; }


       
    }
}