using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.AccessControl;
using System.Net.Configuration;
using System.Reflection;
using System.IO;

namespace MVCAppForSPExec.Models
{
    public class RegistrationModel
    {
        public int RegId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }






        public List<RegistrationModel> GetRegistrationList()
        {
            List<RegistrationModel> lstReg = new List<RegistrationModel>();

            string ConString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(ConString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "uspGetRegistrationList";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lstReg.Add(new RegistrationModel()
                {
                    RegId = Convert.ToInt32(dr["Id"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Password = Convert.ToString(dr["Password"]),
                    Photo = Convert.ToString(dr["Photo"])

                });
            }

            return lstReg;

        }
        public List<RegistrationModel> GetRegListById(int id)
        {
            List<RegistrationModel> lstReg = new List<RegistrationModel>();
            string ConString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("uspGetRegById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegId", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lstReg.Add(new RegistrationModel()
                    {
                        RegId = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        Photo = Convert.ToString(dr["Photo"])

                    });

                }
            }
            return lstReg;
        }


        //this is when there is no photo is uploaded here.
        //public string SaveRegistration(RegistrationModel model)
        //{
        //    string msg = "";
        //    string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //    using(SqlConnection con=new SqlConnection(constring))
        //    {
        //        SqlCommand cmd = new SqlCommand("uspInsertRegistrationRecord", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Id", model.Id);
        //        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", model.LastName);
        //        cmd.Parameters.AddWithValue("@Email", model.Email);
        //        cmd.Parameters.AddWithValue("@Password", model.Password);
        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //    msg = "record saved successfully";
        //    return msg;
        //}

        public string SaveRegistration(HttpPostedFileBase fb, RegistrationModel model)
        {
            string msg = "";
            string fileName = "";
            string filePath = "";
            string sysFileName = "";
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName;
                }
            }
            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("uspInsertRegistrationRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RegId", model.RegId);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Photo", sysFileName);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            msg = "record saved successfully";
            return msg;
        }
        public string DeleteRegistrationRow(int id)
        {
            string msg = "";
            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("uspDeleteRegRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "row deleted successfully";
                return msg;
            }
        }
        public RegistrationModel EditRegistrationRecord(int id)
        {

            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("uspGetRegById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                RegistrationModel model = new RegistrationModel();
                while (dr.Read())
                {
                    model.RegId = Convert.ToInt32(dr["Id"]);
                    model.FirstName = Convert.ToString(dr["FirstName"]);
                    model.LastName = Convert.ToString(dr["LastName"]);
                    model.Email = Convert.ToString(dr["Email"]);
                    model.Password = Convert.ToString(dr["Password"]);
                    model.Photo = Convert.ToString(dr["Photo"]);
                }
                return model;
            }

        }


        //this list get by joining 3 tables
        public List<RegistrationModel> GetRegListWithCityState()
        {
            List<RegistrationModel> regList = new List<RegistrationModel>();

            string ConString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("uspGetRegListWithCityandState", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    regList.Add(new RegistrationModel()
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Photo = Convert.ToString(dr["Photo"]),
                        CityName = Convert.ToString(dr["CityName"]),
                        StateName= Convert.ToString(dr["StateName"])
                    }) ;
                }

            }
            return regList;

        }
    }
}
    