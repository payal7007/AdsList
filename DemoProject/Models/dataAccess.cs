using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Models
{
    public class dataAccess
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }
        public IEnumerable<MyAdvertiseModel> GetAllProductList()
        {
            connection();
            List<MyAdvertiseModel> lstadv = new List<MyAdvertiseModel>();
            SqlCommand cmd = new SqlCommand("GetSellerData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                MyAdvertiseModel product = new MyAdvertiseModel();

                product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                product.productSubCategoryId = Convert.ToInt32(rdr["productSubCategoryId"]);
                product.advertiseTitle = rdr["advertiseTitle"].ToString();
                product.advertiseDescription = rdr["advertiseDescription"].ToString();
                product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                product.areaId = Convert.ToInt32(rdr["areaId"]);
                product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                product.UserId = Convert.ToInt32(rdr["UserId"]);
                product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
                product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                product.imageData = (byte[])(rdr["imageData"]);
                lstadv.Add(product);
            }
            con.Close();
            return lstadv;
        }
        public MyAdvertiseModel GetAdvertiseById(int? advertiseId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("GetSellerData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            MyAdvertiseModel product = null;

            if (rdr.Read())
            {
                product = new MyAdvertiseModel();
                product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                product.productSubCategoryId = Convert.ToInt32(rdr["productSubCategoryId"]);
                product.advertiseTitle = rdr["advertiseTitle"].ToString();
                product.advertiseDescription = rdr["advertiseDescription"].ToString();
                product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                product.areaId = Convert.ToInt32(rdr["areaId"]);
                product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                product.UserId = Convert.ToInt32(rdr["UserId"]);
                product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
                product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                product.imageData = (byte[])(rdr["imageData"]);
            }

            rdr.Close();
            con.Close();

            return product;
        }
        public bool UpdateAdvertise(MyAdvertiseModel model)
        {
            connection();
            // Retrieve the existing record from the database
            var existingAdvertise = con.Advertises.FirstOrDefault(a => a.AdvertiseId == model.AdvertiseId);


            if (existingAdvertise != null)
                    {
                        // Update the properties of the existing record with the edited data
                        existingAdvertise.AdvertiseTitle = model.AdvertiseTitle;
                        existingAdvertise.AdvertiseDescription = model.AdvertiseDescription;

                        // Update more properties as needed

                        // Save the changes to the database
                        con.SaveChanges();

                        return true;
                    }
                
               
               
            }
        
        //public MyAdvertiseModel GetAllDetails(int? advertiseId)
        //{
        //    connection();
        //    SqlCommand cmd = new SqlCommand("GetSellerData", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

        //    con.Open();
        //    SqlDataReader rdr = cmd.ExecuteReader();
        //    MyAdvertiseModel product = null;

        //    if (rdr.Read())
        //    {
        //        product = new MyAdvertiseModel();
        //        product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
        //        product.productSubCategoryId = Convert.ToInt32(rdr["productSubCategoryId"]);
        //        product.advertiseTitle = rdr["advertiseTitle"].ToString();
        //        product.advertiseDescription = rdr["advertiseDescription"].ToString();
        //        product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
        //        product.areaId = Convert.ToInt32(rdr["areaId"]);
        //        product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
        //        product.UserId = Convert.ToInt32(rdr["UserId"]);
        //        product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
        //        product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
        //        product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
        //        product.imageData = (byte[])(rdr["imageData"]);
        //    }

        //    rdr.Close();
        //    con.Close();

        //    return product;
        //}


        public bool DeleteAdvertise(int? advertiseId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteAdvertise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            return rowsAffected > 0;
        }
    }
}

