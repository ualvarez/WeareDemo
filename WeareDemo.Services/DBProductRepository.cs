using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeareDemo.Models;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace WeareDemo.Services
{
    public class DBProductRepository : IProductRepository
    {
        private SqlConnection con;

        public DBProductRepository()
        {
            /*StreamReader r = new StreamReader("appSettings.json");
            string jsonString = r.ReadToEnd();
            AppSettings m = JsonConvert.DeserializeObject<AppSettings>(jsonString);*/

             con = new SqlConnection("server=LAPTOP-4I29AMO5;database=weareDemo;integrated security=true");
        }
        public Product Add(Product newProduct)
        {
            SqlCommand com = new SqlCommand("Sp_product_Insert", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", newProduct.Name);
            com.Parameters.AddWithValue("@Cost", newProduct.Cost);
            com.Parameters.AddWithValue("@Price", newProduct.Price);
            com.Parameters.AddWithValue("@PhotoPath", newProduct.PhotoPath);
            com.Parameters.AddWithValue("@Type", newProduct.Type);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            return newProduct;

        }

        public Product Delete(int id)
        {
            SqlCommand com = new SqlCommand("Sp_product_Delete", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);          
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            return null;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            SqlCommand com = new SqlCommand("Sp_product_sel", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;            
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);


            var List = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Product
            {
                Id = dataRow.Field<int>("Id"),
                Name = dataRow.Field<string>("Name"),
                Cost = Convert.ToDouble(dataRow.Field<decimal>("Cost")),
                PhotoPath = dataRow.Field<string>("PhotoPath"),
                Type = (ProductType)Enum.Parse(typeof(ProductType), dataRow.Field<string>("Type"), true)
            }).ToList();

            return List;
        }

        public Product GetProduct(int id)
        {
            SqlCommand com = new SqlCommand("Sp_product_fnd", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);


            var List = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Product
            {
                Id = dataRow.Field<int>("Id"),
                Name = dataRow.Field<string>("Name"),
                Cost = Convert.ToDouble(dataRow.Field<decimal>("Cost")),
                PhotoPath = dataRow.Field<string>("PhotoPath"),
                Type = (ProductType)Enum.Parse(typeof(ProductType), dataRow.Field<string>("Type"), true)
            }).ToList();

            return List.FirstOrDefault();

        }

        public IEnumerable<TypeHeadCount> ProductCountByType(ProductType? type)
        {
            

          return  new List<TypeHeadCount>() {
            new TypeHeadCount
            {
                Type = ProductType.Service,
                Count = 3
            }};
        }

        public Product Update(Product updatedProduct)
        {
            SqlCommand com = new SqlCommand("Sp_product_Update", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", updatedProduct.Id);
            com.Parameters.AddWithValue("@Name", updatedProduct.Name);
            com.Parameters.AddWithValue("@Cost", updatedProduct.Cost);
            com.Parameters.AddWithValue("@Price", updatedProduct.Price);
            com.Parameters.AddWithValue("@PhotoPath", updatedProduct.PhotoPath);
            com.Parameters.AddWithValue("@Type", updatedProduct.Type);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            return updatedProduct;
        }
    }
}
